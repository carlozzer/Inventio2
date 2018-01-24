using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.WebPages;
using System.Web;
using System.IO;
using RazorEngine.Text;
using System.Web.Security.AntiXss;
using System.Reflection;
using Tc.Inventio.WebParts;

namespace Tc.Inventio.Engine.RazorTemplate
{

    [RequireNamespaces("System.Web.Mvc.Html")]
    public class InventioTemplateBase<T> : TemplateBase<T>, IViewDataContainer
    {
        private HtmlHelper<T> helper = null;
        private ViewDataDictionary viewdata = null;

        public HtmlHelper<T> Html
        {
            get
            {
                if (helper == null)
                {
                    //var writer = this.CurrentWriter; //TemplateBase.CurrentWriter
                    //var context = new ViewContext() { RequestContext = HttpContext.Current.Request.RequestContext, Writer = writer, ViewData = this.ViewData };
                    var context = new ViewContext() { RequestContext = HttpContext.Current.Request.RequestContext, ViewData = this.ViewData };
                    helper = new HtmlHelper<T>(context, this);
                }
                return helper;
            }
        }

        public ViewDataDictionary ViewData
        {
            get
            {
                if (viewdata == null)
                {
                    viewdata = new ViewDataDictionary();
                    viewdata.TemplateInfo = new TemplateInfo() { HtmlFieldPrefix = string.Empty };

                    if (this.Model != null)
                    {
                        viewdata.Model = Model;
                    }
                }
                return viewdata;
            }
            set
            {
                viewdata = value;
            }
        }


        public string RenderInventioWebPart(string AssemblyFullName, string ClassName, object model = null)
        {
            //string path = System.IO.Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Templates", templateName);
            //return Razor.Parse(File.ReadAllText(path), model);

            string ret = string.Empty;

            if (!string.IsNullOrWhiteSpace(AssemblyFullName) && !string.IsNullOrWhiteSpace(ClassName))
            {
                try
                {
                    Assembly asm = Assembly.Load(AssemblyFullName);
                    Type type = asm.GetType(ClassName);
                    InventioWebPart WebPart = (InventioWebPart) Activator.CreateInstance(type, model);
                    
                    // WebPart LifeCycle
                    WebPart.Init();
                    WebPart.DoEvents();
                    WebPart.GetData();
                    ret = WebPart.Render();

                }
                catch (Exception ex)
                { 
                    //debugger
                }
            }

            return ret;
        }



       
    }

    
} 


