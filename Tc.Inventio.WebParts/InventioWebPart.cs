using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tc.Inventio.WebParts
{
    public class InventioWebPart : MarshalByRefObject
    {
        public virtual void Init() { }
        public virtual void DoEvents() { }
        public virtual void GetData() {  }
        public virtual string Render() { return string.Empty; }
    }
}
