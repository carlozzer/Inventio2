using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tc.Inventio.Data.Entities;

namespace Tc.Inventio.UI.Models
{
    public class PagesModel
    {
        public InventioNavigationNode           Navigation      { get; set; }
        public IEnumerable<SelectListItem>      Layouts         { get; set; }

        public string                           Path            { get; set; }
        public string                           Type            { get; set; }
        public string                           Title           { get; set; }
        public Nullable<int>                    ObjectId        { get; set; }
        public Nullable<int>                    ParentId        { get; set; }
        public string                           Action          { get; set; }
        public int                              LayoutId        { get; set; }

        public string data_jstree
        {
            get 
            { 
                string ret = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.Type))
                {
                    if (this.Type.ToLower() == "page")
                    {
                        ret = "jstree-file";
                    }
                }

                return ret;
            }
        }



    }
}