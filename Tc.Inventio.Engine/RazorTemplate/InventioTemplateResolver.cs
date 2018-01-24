using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tc.Inventio.Data.Entities;
using Tc.Inventio.Engine.Pages;

namespace Tc.Inventio.Engine.RazorTemplate
{

    class InventioTemplateResolver : ITemplateResolver
    {
        public InventioTemplateResolver() { }
        
        public string Resolve(string name)
        {
            string ret = "MASTER NOT FOUND";

            InventioLayout layout = LayoutWorker.SelectByTitle(name, LayoutWorker.RepositoryWebConfig);
            if (layout != null)
            {
                ret = System.Text.Encoding.UTF8.GetString(layout.Body);
            }

            return ret;
        }
    }

    
}
