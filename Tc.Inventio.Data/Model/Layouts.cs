//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tc.Inventio.Data.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Layouts
    {
        public Layouts()
        {
            this.Pages = new HashSet<Pages>();
        }
    
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PreviewUrl { get; set; }
        public string Body { get; set; }
    
        public virtual ICollection<Pages> Pages { get; set; }
    }
}
