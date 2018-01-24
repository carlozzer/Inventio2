using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Tc.Inventio.Engine.Config
{
    public class InventioConfigManager
    {
        // TODO repository assembly name in web.config ???

        private static bool IsEntityFramework
        {
            get
            {
                bool ret = false;

                if (WebConfigurationManager.AppSettings["InventioRepository"] != null)
                {
                    if (WebConfigurationManager.AppSettings["InventioRepository"].ToString().ToLower() == "ef")
                    {
                        ret = true;
                    }
                }

                return ret;
            }
        }

        public static string LayoutRepositoryClassName
        {
            get { return IsEntityFramework ? "Tc.Inventio.Data.Concrete.LayoutRepositoryEF" : "LayoutRepositoryXml"; }
        }

        public static string NavigationRepositoryClassName
        {
            get { return IsEntityFramework ? "Tc.Inventio.Data.Concrete.NavigationRepositoryEF" : "NavigationRepositoryXml"; }
        }

        public static string PageRepositoryClassName
        {
            get { return IsEntityFramework ? "Tc.Inventio.Data.Concrete.PageRepositoryEF" : "PageRepositoryXml"; }
        }


    }
}
