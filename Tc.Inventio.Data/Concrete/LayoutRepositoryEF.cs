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
    public class LayoutRepositoryEF : ILayoutRepository
    {
        #region ADAPTERS
        
        public Layouts EFAdapter(string title, string description, byte[] body, string PreviewUrl)
        {
            Layouts ret = null;

            ret = new Layouts();
            ret.Title = title;
            ret.Description = description;
            ret.Body = Convert.ToBase64String(body);
            ret.PreviewUrl = PreviewUrl;

            return ret;
        }

        public Layouts EFAdapter(InventioLayout layout)
        {
            Layouts ret = null;

            ret = new Layouts();
            ret.Id = layout.Id;
            ret.Title = layout.Title;
            ret.Description = layout.Description;
            ret.Body = Convert.ToBase64String(layout.Body);
            ret.PreviewUrl = layout.PreviewUrl;

            return ret;
        }

        public InventioLayout InventioAdapter(Layouts eflayout)
        {
            InventioLayout ret = null;

            if (eflayout != null)
            {
                ret = new InventioLayout();
                ret.Id = eflayout.Id;
                ret.Title = eflayout.Title;
                ret.Description = eflayout.Description;
                ret.Body = Convert.FromBase64String(eflayout.Body);
                ret.PreviewUrl = eflayout.PreviewUrl;
            }

            return ret;
        }

        #endregion

        #region CRUD
        
        public Nullable<int> Create(InventioLayout layout)
        {
            Nullable<int> ret = null;

            Layouts data = EFAdapter(layout);

            using (InventioEntities context = new InventioEntities())
            {
                context.Layouts.Add(data);
                context.SaveChanges();
                ret = data.Id;
            }

            return ret;
        }

        
        
        
        
        public InventioLayout Read(int id)
        {
            InventioLayout ret = new InventioLayout();

            using (InventioEntities context = new InventioEntities())
            {
                Layouts selected = context.Layouts.Where<Layouts>(s => s.Id == id).FirstOrDefault<Layouts>();
                if (selected != null) ret = InventioAdapter(selected);
            }

            return ret;

        }

        public void Update(InventioLayout layout)
        {
            // TODO REVISAR NO TESTEADO

            Layouts data = EFAdapter(layout);

            using (InventioEntities context = new InventioEntities())
            {
                context.Layouts.Attach(data);
                context.Entry(data).State = EntityState.Modified;
                context.SaveChanges();
            }
            
        }

        public void Delete(int Id)
        {
            using (InventioEntities context = new InventioEntities())
            {
                Layouts selected = context.Layouts.FirstOrDefault<Layouts>(l => l.Id == Id);
                if (selected != null)
                {
                    context.Layouts.Attach(selected);
                    context.Layouts.Remove(selected);
                    context.SaveChanges();
                }
            }
        }

        public InventioLayout SelectById(int id)
        {
            InventioLayout ret = null;

            ret = Read(id); 
            if (ret == null)
            {
                ret = new InventioLayout();
                ret.Title = "Not found";
                ret.Body = Encoding.UTF8.GetBytes("<h1>Página no encontrada</h1><p>Hola @ViewBag.Name</p>");
            }

            return ret;
        }

        public InventioLayout SelectByTitle(string title)
        {
            InventioLayout ret = null;

            if (!string.IsNullOrWhiteSpace(title))
            {
                using (InventioEntities context = new InventioEntities())
                {
                    Layouts selected = context.Layouts.Where<Layouts>(s => s.Title.ToLower() == title.ToLower()).FirstOrDefault<Layouts>();
                    if (selected != null) ret = InventioAdapter(selected);
                }
            }

            return ret;
        }

        #endregion

        #region SELECT

        public List<InventioLayout> SelectAll()
        {
            List<InventioLayout> ret = null;

            using (InventioEntities context = new InventioEntities())
            {
                if (context.Layouts != null && context.Layouts.Count() > 0)
                {
                    ret = new List<InventioLayout>();

                    foreach (Layouts layout in context.Layouts)
                    {
                        InventioLayout newlayout = InventioAdapter(layout);
                        if (newlayout != null)
                        {
                            ret.Add(newlayout);
                        }
                    }
                }
            }

            return ret;
        }

        #endregion


    }
}
