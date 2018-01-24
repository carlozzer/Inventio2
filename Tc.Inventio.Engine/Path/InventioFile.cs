using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using Tc.Inventio.Engine.Pages;

namespace Tc.Inventio.Engine.Path
{
    public class InventioFile : VirtualFile
    {
        private string _path;

        public InventioFile(string path) : base(path)
        {
            this._path = path;
        }

        public override Stream Open()
        {
            return PagesManager.GetInventioPageStream(this._path);
        }
    }
}
