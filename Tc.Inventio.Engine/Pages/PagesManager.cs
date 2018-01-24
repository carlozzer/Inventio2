using RazorEngine;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;
using Tc.Inventio.Data.Entities;
using Tc.Inventio.Engine.RazorTemplate;

namespace Tc.Inventio.Engine.Pages
{
    public class PagesManager
    {
        #region LAYOUTS

        public static Nullable<int> AddLayout(InventioLayout layout)
        {
            Nullable<int> ret = null;

            if (layout != null)
            {
                InventioLayout hit = LayoutWorker.SelectByTitle(layout.Title, LayoutWorker.RepositoryWebConfig);
                if (hit == null)
                {
                    ret = LayoutWorker.Add(layout, LayoutWorker.RepositoryWebConfig);
                }
                else
                {
                    layout.Id = hit.Id;
                    LayoutWorker.Update(layout, LayoutWorker.RepositoryWebConfig);
                    ret = layout.Id;
                }
            }

            return ret;
        }

        public static List<InventioLayout> SelectAllLayouts()
        {
            List<InventioLayout> ret = null;

            ret = LayoutWorker.SelectAll(LayoutWorker.RepositoryWebConfig);

            return ret;
        }

        #endregion

        #region Navigation

        public static InventioNavigationNode ReadNavigationNodes()
        {
            return NavigationWorker.SelectAll(NavigationWorker.RepositoryWebConfig);
        }

        public static void AddFolder(string Path, string Title, Nullable<int> ParentId)
        {
            InventioNavigationNode NewNode = new InventioNavigationNode();
            NewNode.NodeType = InventioNavigationNodeType.Folder;
            NewNode.ParentId = ParentId;
            NewNode.Url = Path + "/" + Tc.Inventio.Framework.Web.UrlHelper.Slug(Title); 
            NewNode.Title = Title;
            NavigationWorker.Add(NewNode, NavigationWorker.RepositoryWebConfig);
        }

        #endregion

        #region PAGES

        public static void AddPage(string Path, string title, int IdLayout, Nullable<int> ParentId)
        { 
            bool ok = !string.IsNullOrWhiteSpace(Path) && !string.IsNullOrWhiteSpace(title);

            if (ok)
            {
                // añadimos página
                InventioPage NewPage = new InventioPage();
                NewPage.Path = Path + "/" + Tc.Inventio.Framework.Web.UrlHelper.Slug(title);
                NewPage.Title = title;
                NewPage.IdLayout = IdLayout;
                PageWorker.Add(NewPage, PageWorker.RepositoryWebConfig);

                // añadimos entrada en navigation
                InventioNavigationNode NewNode = new InventioNavigationNode();
                NewNode.NodeType = InventioNavigationNodeType.Page;
                NewNode.ParentId = ParentId;
                NewNode.Url = NewPage.Path;
                NewNode.Title = title;
                NavigationWorker.Add(NewNode, NavigationWorker.RepositoryWebConfig);
            }
        }

        public static void RemovePage(string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                InventioNavigationNode nav = NavigationWorker.SelectByPath(path, NavigationWorker.RepositoryWebConfig);
                if (nav != null) { NavigationWorker.Remove(nav.Id, NavigationWorker.RepositoryWebConfig); } // eliminamos navigation 
                InventioPage page = PageWorker.SelectByPath(path, PageWorker.RepositoryWebConfig);
                if (page != null) { PageWorker.Remove(page.Id, PageWorker.RepositoryWebConfig); } // eliminamos página
            }
        }

        public static bool IsInventioPage(string path)
        {
            bool ret = false;

            if (!string.IsNullOrWhiteSpace(path))
            {
                InventioPage selected = PageWorker.SelectByPath(path, PageWorker.RepositoryWebConfig);
                ret = selected != null;
            }

            return ret;
        }

        public static Stream GetInventioPageStream(string path)
        {
            Stream ret = null;

            InventioPage page = PageWorker.SelectByPath(path, PageWorker.RepositoryWebConfig);
            if (page != null)
            {
                InventioLayout layout = LayoutWorker.SelectById(page.IdLayout, LayoutWorker.RepositoryWebConfig);
                if (layout != null)
                {
                    string raw = System.Text.Encoding.UTF8.GetString(layout.Body);
                    string parsed = string.Empty;

                    // BEGIN RazorEngine Template // TODO igual hay que meterlo en global asax
                    var config = new RazorEngine.Configuration.TemplateServiceConfiguration
                    {
                        //BaseTemplateType = typeof(InventioTemplateBase<>)
                        BaseTemplateType = typeof(InventioTemplateBase<>),
                        Resolver = new InventioTemplateResolver()
                    };

                    using (var service = new TemplateService(config))
                    {
                        RazorEngine.Razor.SetTemplateService(service);
                        parsed = Razor.Parse(raw);
                    }
                    // END RazorEngine

                    ret = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(parsed), false);
                }
            }

            return ret;
        }

        #endregion


    }
}
