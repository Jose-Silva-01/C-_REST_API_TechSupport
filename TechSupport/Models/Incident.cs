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
    
    public partial class Incident
    {
        public int IncidentID { get; set; }
        public int CustomerID { get; set; }
        public string ProductCode { get; set; }
        public Nullable<int> TechID { get; set; }
        public System.DateTime DateOpened { get; set; }
        public Nullable<System.DateTime> DateClosed { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
        public virtual Technician Technician { get; set; }
    }
}
