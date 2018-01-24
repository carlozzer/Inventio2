using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tc.Inventio.Data.Entities
{
    public class InventioPage
    {
        public int                      Id                  { get; set; }
        public Guid                     Page_Guid           { get; set; }
        public string                   Path                { get; set; }
        public string                   Title               { get; set; }
        public int                      IdLayout            { get; set; } 
        public InventioPageStates       State               { get; set; }
        public string                   PubVersion          { get; set; }
        public string                   MetaData            { get; set; }
        public string                   MetaDataVersion     { get; set; }
        public InventioPageAction       Action              { get; set; }
        public int                      IdUser              { get; set; }
    }
}
