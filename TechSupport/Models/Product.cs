//------------------------------------------------------------------------------
// <auto-generated>
//    O código foi gerado a partir de um modelo.
//
//    Alterações manuais neste arquivo podem provocar comportamento inesperado no aplicativo.
//    Alterações manuais neste arquivo serão substituídas se o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TechSupport.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public Product()
        {
            this.Incidents = new HashSet<Incident>();
            this.Registrations = new HashSet<Registration>();
        }
    
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public decimal Version { get; set; }
        public System.DateTime ReleaseDate { get; set; }
    
        public virtual ICollection<Incident> Incidents { get; set; }
        public virtual ICollection<Registration> Registrations { get; set; }
    }
}
