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
    public class NavigationRepositoryEF : INavigationRepository
    {
        #region ADAPTERS
        
        public Navigation EFAdapter(InventioNavigationNode node)
        {
            Navigation ret = null;

            if (node != null)
            {
                ret = new Navigation();
                ret.Id = node.Id;
                ret.ParentId = node.ParentId;
                ret.Path = node.Url;
                ret.NodeType = (int)node.NodeType;
                ret.Title = node.Title;
                ret.MetaData = node.MetaData;
                ret.ObjectId = node.ObjectId;
            }

            return ret;
        }

        public InventioNavigationNode InventioAdapter(Navigation nav)
        {
            InventioNavigationNode ret = null;

            if (nav != null)
            {
                ret = new InventioNavigationNode();

                ret.Id = nav.Id;

                ret.ParentId = nav.ParentId;
                ret.Url = nav.Path;
                ret.NodeType = (InventioNavigationNodeType)nav.NodeType;
                ret.Title = nav.Title;
                ret.MetaData = nav.MetaData;
                ret.ObjectId = nav.ObjectId;
                // TODO
                //ret.ChildNodes = ChildsToString(node.ChildNodes);
            }

            return ret;
        }

        #endregion

        #region CRUD
        
        public Nullable<int> Create(InventioNavigationNode node)
        {
            Nullable<int> ret = null;

            Navigation data = EFAdapter(node);

            using (InventioEntities context = new InventioEntities())
            {
                context.Navigation.Add(data);
                context.SaveChanges();
                ret = data.Id;
            }

            return ret;
        }

        



        public void Update(InventioNavigationNode node)
        {
            Navigation data = EFAdapter(node);

            using (InventioEntities context = new InventioEntities())
            {
                context.Navigation.Attach(data);
                context.Entry(data).State = EntityState.Modified;
                context.SaveChanges();
            }
            
        }

        public void Delete(int Id)
        {
            using (InventioEntities context = new InventioEntities())
            {
                Navigation selected = context.Navigation.FirstOrDefault<Navigation>(n => n.Id == Id);
                if (selected != null)
                {
                    context.Navigation.Attach(selected);
                    context.Navigation.Remove(selected);
                    context.SaveChanges();
                }
            }
        }

        #endregion

        public InventioNavigationNode SelectById(int id)
        {
            return Read(id); 
        }

        public InventioNavigationNode SelectByPath(string path)
        {
            InventioNavigationNode ret = null;

            if (!string.IsNullOrWhiteSpace(path))
            {
                using (InventioEntities context = new InventioEntities())
                {
                    Navigation selected = context.Navigation.FirstOrDefault<Navigation>(n => n.Path.ToLower() == path.ToLower());
                    if (selected != null)
                    {
                        ret = Read(selected.Id);
                    }
                }
            }

            return ret;
        }


        public InventioNavigationNode Read(int id)
        {
            InventioNavigationNode ret = new InventioNavigationNode();

            using (InventioEntities context = new InventioEntities())
            {
                Navigation selected = context.Navigation.Where<Navigation>(s => s.Id == id).FirstOrDefault<Navigation>();
                if (selected != null)
                {
                    ret = InventioAdapter(selected);
                    ret.ChildNodes = ReadChilds(selected.Id);

                    //// Hijos
                    if (ret.ChildNodes != null && ret.ChildNodes.Count > 0)
                    {
                        for (int i = 0; i < ret.ChildNodes.Count; i++)
                        {
                            ret.ChildNodes[i] = Read(ret.ChildNodes[i].Id);
                        }
                    }
                }
            }

            return ret;
        }

        public List<InventioNavigationNode> ReadChilds(Nullable<int> ParentId)
        {
            List<InventioNavigationNode> ret = null;

            using (InventioEntities context = new InventioEntities())
            {
                List<Navigation> selected = context.Navigation.Where<Navigation>(s => s.ParentId == ParentId).ToList<Navigation>();
                if (selected != null && selected.Count > 0)
                {
                    ret = new List<InventioNavigationNode>();
                    foreach (Navigation child in selected)
                    {
                        ret.Add(InventioAdapter(child));
                    }
                }
            }

            return ret;

        }


        #region SELECT

        //public List<InventioLayout> SelectAll()
        //{
        //    List<entities.InventioLayout> ret = null;

        //    using (InventioEntities context = new InventioEntities())
        //    {
        //        if (context.Layouts != null && context.Layouts.Count() > 0)
        //        {
        //            ret = new List<entities.InventioLayout>();

        //            foreach (Layouts layout in context.Layouts)
        //            {
        //                entities.InventioLayout newlayout = InventioAdapter(layout);
        //                if (newlayout != null)
        //                {
        //                    ret.Add(newlayout);
        //                }
        //            }
        //        }
        //    }

        //    return ret;
        //}

        #endregion


    }
}
