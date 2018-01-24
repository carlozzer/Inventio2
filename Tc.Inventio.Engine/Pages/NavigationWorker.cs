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
    public class NavigationWorker
    {
        
        public static INavigationRepository RepositoryWebConfig
        {
            get { return (INavigationRepository)Activator.CreateInstance("Tc.Inventio.Data", InventioConfigManager.NavigationRepositoryClassName).Unwrap(); }
        }

        #region SELECT

        public static InventioNavigationNode SelectAll(INavigationRepository Repository)
        { 
            return Repository.SelectById(1); //TODO SelectRoot() busca el nodo cuyo padre es null y lo llena recursivamente
        }

        public static InventioNavigationNode SelectByPath(string path, INavigationRepository Repository)
        {
            return Repository.SelectByPath(path);
        }

        #endregion

        #region ADD 

        public static Nullable<int> Add(InventioNavigationNode node, INavigationRepository Repository)
        {
            Nullable<int> ret = null;

            if (node != null)
            {
                ret = Repository.Create(node);
            }

            return ret;
        }

        #endregion

        #region REMOVE LAYOUT

        public static void Remove(int Id, INavigationRepository Repository)
        {
            Repository.Delete(Id);
        }

        #endregion
    }
}
