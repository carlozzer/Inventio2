using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tc.Inventio.Data.Abstract;
using Tc.Inventio.Data.Entities;
using Tc.Inventio.Engine.Config;

namespace Tc.Inventio.Engine.Pages
{
    public class PageWorker
    {
        #region SELECT

        public static IPageRepository RepositoryWebConfig
        {
            get
            {
                return (IPageRepository)Activator.CreateInstance("Tc.Inventio.Data", InventioConfigManager.PageRepositoryClassName).Unwrap(); // 1st para, null = executing assembly
            }
        }

        //public static Inventio SelectById(int id, ILayoutRepository Repository)
        //{
        //    return Repository.SelectById(id);
        //}

        //public static List<InventioLayout> SelectAll(ILayoutRepository Repository)
        //{
        //    return Repository.SelectAll();
        //}

        #endregion

        #region ADD LAYOUT

        public static Nullable<int> Add(InventioPage page, IPageRepository Repository)
        {
            Nullable<int> ret = null;

            if (page != null)
            {
                ret = Repository.Create(page);
            }

            return ret;
        }

        #endregion  

        #region SELECT

        public static InventioPage SelectByPath(string path, IPageRepository repository)
        {
            InventioPage ret = null;

            if (!string.IsNullOrWhiteSpace(path))
            {
                ret = repository.SelectByPath(path.Replace("~", string.Empty));
            }


            return ret;
        }

        #endregion

        #region DELETE

        public static void Remove(string path, IPageRepository repository)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                InventioPage selected = repository.SelectByPath(path.Replace("~", string.Empty));
                if (selected != null)
                {
                    repository.Delete(selected.Id);
                }
            }
        }

        public static void Remove(int id, IPageRepository repository)
        {
            repository.Delete(id);
        }

        #endregion

    }
}
