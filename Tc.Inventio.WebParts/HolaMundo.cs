using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tc.Inventio.WebParts
{
    public class HolaMundo : InventioWebPart
    {
        private string source;

        public HolaMundo()
        {
            this.source = "NO PARAMS";
        }

        public HolaMundo(object model)
        {
            if (model != null)
            {
                this.source = model.ToString();
            }
        }

        public override void GetData()
        {
            
        }

        public override string Render()
        {
            return this.source;
        }
    }
}
