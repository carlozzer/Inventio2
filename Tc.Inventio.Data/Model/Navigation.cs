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
    
    public partial class Navigation
    {
        public int Id { get; set; }
        public Nullable<int> ParentId { get; set; }
        public string Path { get; set; }
        public int NodeType { get; set; }
        public string Title { get; set; }
        public string MetaData { get; set; }
        public Nullable<int> ObjectId { get; set; }
    }
}
