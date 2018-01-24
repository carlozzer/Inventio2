
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
    public class LayoutWorker
    {
        public static ILayoutRepository RepositoryWebConfig
        {
            get
            {
                return (ILayoutRepository)Activator.CreateInstance("Tc.Inventio.Data", InventioConfigManager.LayoutRepositoryClassName).Unwrap(); // 1st para, null = executing assembly
            }
        }

        #region SELECT

        public static InventioLayout SelectById(int id, ILayoutRepository Repository)
        {
            return Repository.SelectById(id);
        }

        public static InventioLayout SelectByTitle(string title, ILayoutRepository Repository)
        {
            return Repository.SelectByTitle(title);
        }

        public static List<InventioLayout> SelectAll(ILayoutRepository Repository)
        {
            return Repository.SelectAll();
        }

        #endregion

        #region ADD LAYOUT

        public static Nullable<int> Add(InventioLayout layout, ILayoutRepository Repository)
        {
            Nullable<int> ret = null;

            if (layout != null)
            {
                ret = Repository.Create(layout);
            }

            return ret;
        }

        #endregion

        #region REMOVE LAYOUT

        public static void Remove(int Id, ILayoutRepository Repository)
        {
            Repository.Delete(Id);
        }

        #endregion

        #region UPDATE

        public static void Update(InventioLayout layout, ILayoutRepository Repository)
        {
            Repository.Update(layout);
        }

        #endregion
    }
}
