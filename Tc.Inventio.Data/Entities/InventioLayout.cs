using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tc.Inventio.Data.Entities
{
    public class InventioLayout
    {
        public int          Id                  { get; set; }
        public string       Title               { get; set; }
        public string       Description         { get; set; }
        public byte[]       Body                { get; set; }
        public string       PreviewUrl          { get; set; }
    }
}
