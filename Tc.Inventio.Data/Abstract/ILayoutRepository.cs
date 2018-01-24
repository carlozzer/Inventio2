using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tc.Inventio.Data.Entities;

namespace Tc.Inventio.Data.Abstract
{
    public interface ILayoutRepository
    {
        // CRUD
        Nullable<int>           Create          (InventioLayout layout);
        InventioLayout          Read            (int id);
        void                    Update          (InventioLayout layout);
        void                    Delete          (int id);

        // SELECT
        List<InventioLayout>    SelectAll       ();
        InventioLayout          SelectById      (int id);
        InventioLayout          SelectByTitle   (string title);
    }
}
