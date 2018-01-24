using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Tc.Inventio.Data.Abstract;
using Tc.Inventio.Data.Entities;

namespace Tc.Inventio.Data.Concrete
{
    public class LayoutRepositoryXml : ILayoutRepository
    {
        public static string XmlFile
        {
            get
            {
                //return HttpContext.Current.Server.MapPath("/Layouts.xml");
                return string.Empty;
            }
        }

        public Nullable<int> Create(InventioLayout layout)
        {
            DataTable dt = new DataTable();
            
            // Check if file exists
            if (File.Exists(XmlFile))
            {
                //dt.ReadXml(XmlFile);
                File.Delete(XmlFile);
            }
            //else
            //{
                dt.TableName = "Layouts";
                //dt.Columns.Add("Id", typeof(int64));
                dt.Columns.Add("Title", typeof(String));
                dt.Columns.Add("Description", typeof(String));
                dt.Columns.Add("Body", typeof(String));
                dt.Columns.Add("PreviewUrl", typeof(String));
            //}

            DataRow NewRow = dt.NewRow();
            NewRow["Title"] = layout.Title;
            NewRow["Description"] = layout.Description;
            NewRow["Body"] = Convert.ToBase64String(layout.Body);
            NewRow["PreviewUrl"] = layout.PreviewUrl;
            dt.Rows.Add(NewRow);

            dt.WriteXml(XmlFile);

            return 1;
        }

        public InventioLayout Read(int id)
        {
            //// TODO
            InventioLayout ret = new InventioLayout();
            //ret.Title = "Este es el título de la página";
            //ret.Body = BytesHelper.GetBytes("hola");

            return ret;
        }

        public void Update(InventioLayout layout)
        {
            throw new NotImplementedException();
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public InventioLayout SelectById(int id)
        {
            // TODO 

            InventioLayout ret = new InventioLayout();

            if (File.Exists(XmlFile))
            {
                DataSet ds = new DataSet();
                ds.ReadXml(XmlFile);
                ret.Title       = ds.Tables[0].Rows[0]["Title"] as string ?? string.Empty;
                ret.Body = Convert.FromBase64String(ds.Tables[0].Rows[0]["Body"].ToString());
                ret.PreviewUrl  = ds.Tables[0].Rows[0]["PreviewUrl"].ToString();
            }
            else
            {
                ret.Title = "Not found";
                ret.Body = Encoding.UTF8.GetBytes("<h1>Página no encontrada</h1><p>Hola @ViewBag.Name</p>");
            }

            return ret;
        }

        public List<InventioLayout> SelectAll()
        {
            return null; // TODO not implemented
        }

        public InventioLayout SelectByTitle(string title)
        {
            return null;
        }
    }
}
