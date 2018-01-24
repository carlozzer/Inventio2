using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tc.Inventio.Data.Entities;
using Tc.Inventio.Engine.Pages;
using Tc.Inventio.Framework.IO;
using Tc.Inventio.UI.Models;

namespace Tc.Inventio.UI.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

        // LAYOUTS -------------------------------------------------------------------------------

        public ActionResult Layouts()
        {
            List<InventioLayout> model = LayoutWorker.SelectAll(LayoutWorker.RepositoryWebConfig);
            return View(model);
        }

        [HttpPost]
        public ActionResult Layouts(HttpPostedFileBase file)
        {
            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                InventioLayout layout = new InventioLayout();
                layout.Title = Path.GetFileName(file.FileName);
                layout.PreviewUrl = string.Empty;
                layout.Body = BytesHelper.StreamToByteArray(file.InputStream);

                PagesManager.AddLayout(layout);
            }

            return RedirectToAction("Layouts");
        }

        public ActionResult DeleteLayout(int id)
        { 
            // delete layout by id
            LayoutWorker.Remove(id, LayoutWorker.RepositoryWebConfig);

            // reload
            return RedirectToAction("Layouts");

        }



        // PAGES ----------------------------------------------------------------------------

        private List<SelectListItem> GetLayoutsCombo()
        {
            List<SelectListItem> ret = null;

            List<InventioLayout> layouts = PagesManager.SelectAllLayouts();
            if (layouts != null && layouts.Count > 0)
            {
                ret = new List<SelectListItem>();

                foreach (InventioLayout layout in layouts)
                {
                    SelectListItem item = new SelectListItem();
                    item.Text = layout.Title;
                    item.Value = layout.Id.ToString();
                    item.Selected = false;
                    ret.Add(item);
                }
            }

            return ret;
        }

        public ActionResult Pages()
        {
            PagesModel model = new PagesModel();
            model.Navigation = PagesManager.ReadNavigationNodes();
            model.Layouts = GetLayoutsCombo();
            return View(model);
        }

        [HttpPost]
        public ActionResult Pages(PagesModel model)
        {
            // Ejecuta acciones
            if (model != null && !string.IsNullOrWhiteSpace(model.Action))
            {
                switch (model.Action.ToLower().Replace("action-",string.Empty))
                {
                    case "add-folder":
                        PagesManager.AddFolder(model.Path, model.Title, model.ParentId);
                        break;
                    case "add-page":
                        PagesManager.AddPage(model.Path, model.Title, model.LayoutId, model.ParentId);
                        break;
                    case "remove-page":
                        PagesManager.RemovePage(model.Path);
                        break;
                    default:
                        break;
                }
            }

            model.Navigation = PagesManager.ReadNavigationNodes();
            model.Layouts = GetLayoutsCombo();
            return View(model);
        }

    }
}
