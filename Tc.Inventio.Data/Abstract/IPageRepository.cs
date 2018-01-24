using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tc.Inventio.Data.Entities;

namespace Tc.Inventio.Data.Abstract
{
    public interface IPageRepository
    {
        Nullable<int>   Create      (InventioPage page);
        InventioPage    Read        (int id);
        void            Update      (InventioPage page);
        void            Delete      (int Id);

        InventioPage SelectByPath(string path);
    }
}
