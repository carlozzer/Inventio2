using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tc.Inventio.Data.Entities;

namespace Tc.Inventio.Data.Abstract
{
    public interface INavigationRepository
    {
        // CRUD
        Nullable<int>               Create      (InventioNavigationNode node);
        InventioNavigationNode      Read        (int id);
        void                        Update      (InventioNavigationNode node);
        void                        Delete      (int Id);

        // SELECT
        //List<InventioLayout> SelectAll();
        InventioNavigationNode      SelectById  (int id);
        InventioNavigationNode      SelectByPath(string path);
    }
}
