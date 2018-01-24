using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Tc.Inventio.Data.Abstract;
using Tc.Inventio.Data.Entities;
using Tc.Inventio.Data.Model;

namespace Tc.Inventio.Data.Concrete
{
    public class PageRepositoryEF : IPageRepository
    {
        #region ADAPTERS

        

        //private List<InventioNavigationNode> StringToChilds(string childs)
        //{
        //    List<InventioNavigationNode> ret = string.Empty;

        //    if (!string.IsNullOrWhiteSpace(childs))
        //    {
        //        foreach (InventioNavigationNode node in childs)
        //        {
        //            ret = string.Concat(";", ret, node.Id);
        //        }

        //        ret = ret.Substring(1); // quitamos el primer ';'
        //    }
            
                
            

        //    return ret;
        //}
        
        public Pages EFAdapter(InventioPage page)
        {
            Pages ret = null;

            if (page != null)
            {
                ret = new Pages();
                ret.Id              = page.Id;
                ret.GUID            = page.Page_Guid != null ? page.Page_Guid.ToString("B") : string.Empty ;
                ret.Path            = page.Path;
                ret.Title           = page.Title;
                ret.LayoutId        = page.IdLayout;
                ret.State           = (int) page.State;
                ret.PubVersion      = page.PubVersion;
                ret.Metadata        = page.MetaData;
                ret.MetadataVersion = page.MetaDataVersion;
                ret.Action          = (int) page.Action;
                ret.IdUser          = page.IdUser;  
            }

            return ret;
        }

        public InventioPage InventioAdapter(Pages page)
        {
            InventioPage ret = null;

            if (page != null)
            {
                ret = new InventioPage();

                ret.Id = page.Id;
                //ret.Page_Guid = page.GUID;  // TODO 
                ret.Path = page.Path;
                ret.Title = page.Title;
                ret.IdLayout = page.LayoutId;
                ret.State = (InventioPageStates) page.State;
                ret.PubVersion = page.PubVersion;
                ret.MetaData = page.Metadata;
                ret.MetaDataVersion = page.MetadataVersion;
                ret.Action = (InventioPageAction) page.Action;
                ret.IdUser = page.IdUser.HasValue ? page.IdUser.Value : -1;  
            }

            return ret;
        }

        #endregion

        #region CRUD
        
        public Nullable<int> Create(InventioPage page)
        {
            Nullable<int> ret = null;

            // TODO CHECK PATH no existe ya !!!!

            Pages data = EFAdapter(page);

            using (InventioEntities context = new InventioEntities())
            {
                context.Pages.Add(data);
                context.SaveChanges();
                ret = data.Id;
            }

            return ret;
        }

        public InventioPage Read(int id)
        {
            InventioPage ret = new InventioPage();

            using (InventioEntities context = new InventioEntities())
            {
                Pages selected = context.Pages.Where<Pages>(p => p.Id == id).FirstOrDefault<Pages>();
                if (selected != null) ret = InventioAdapter(selected);
            }

            return ret;

        }

        public void Update(InventioPage page)
        {
            Pages data = EFAdapter(page);

            using (InventioEntities context = new InventioEntities())
            {
                context.Pages.Attach(data);
                context.Entry(data).State = EntityState.Modified;
                context.SaveChanges();
            }

        }

        public void Delete(int Id)
        {
            using (InventioEntities context = new InventioEntities())
            {
                Pages selected = context.Pages.FirstOrDefault<Pages>(p => p.Id == Id);
                if (selected != null)
                {
                    context.Pages.Attach(selected);
                    context.Pages.Remove(selected);
                    context.SaveChanges();
                }
            }
        }

        public InventioPage SelectById(int id)
        {
            InventioPage ret = null;

            ret = Read(id);
            if (ret == null)
            {
                ret = new InventioPage();
                ret.Title = "Not found";
                //ret.Body = Encoding.UTF8.GetBytes("<h1>404</h1><p>Hola @ViewBag.Name</p>");
            }

            return ret;
        }

        #endregion

        #region SELECT

        public InventioPage SelectByPath(string path)
        {
            InventioPage ret = null;

            using (InventioEntities context = new InventioEntities())
            {
                Pages selected = context.Pages.FirstOrDefault<Pages>(p => p.Path.ToLower() == path.ToLower());
                if (selected != null) ret = InventioAdapter(selected);
            }
            
            return ret;
        }

        #endregion


    }
}
