using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using Tc.Inventio.Engine.Pages;

namespace Tc.Inventio.Engine.Path
{
    public class InventioPathProvider : VirtualPathProvider
    {
        public bool IsInventio(string virtualPath)
        {
            //return virtualPath.Contains("inventio");
            return PagesManager.IsInventioPage(virtualPath);
        }

        public override bool FileExists(string virtualPath)
        {
            // IMPORTANTE EL FILTRO DEL CSHTML PARA QUE SOLO ATIENDA LAS VISTAS
            //if (IsInventio(virtualPath) && virtualPath.EndsWith(".cshtml"))
            if (IsInventio(virtualPath))
            {
                return true;
            }
            else
            {
                return Previous.FileExists(virtualPath);
            }
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            if (IsInventio(virtualPath))
            {
                return new InventioFile(virtualPath);
            }
            else
            {
                return Previous.GetFile(virtualPath);
            }
        }

        public override System.Web.Caching.CacheDependency GetCacheDependency(string virtualPath, System.Collections.IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            if (IsInventio(virtualPath))
            {
                return null;
            }
            else
            {
                return Previous.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
            }

        }

    }
}
