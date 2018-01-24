using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tc.Inventio.Data.Entities
{
    public class InventioNavigationNode
    {
        public int                              Id                  { get; set; }
        public Nullable<int>                    ParentId            { get; set; }
        public string                           Title               { get; set; }
        public string                           Url                 { get; set; }
        public InventioNavigationNodeType       NodeType            { get; set; }
        public string                           MetaData            { get; set; }
        public Nullable<int>                    ObjectId            { get; set; }
        public List<InventioNavigationNode>     ChildNodes          { get; set; }
        
        public string data_jstree
        {
            get { return this.NodeType == InventioNavigationNodeType.Page ? "{\"icon\" : \"/Images/file.png\"}" : string.Empty; }
        }
    }
}
