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
    
    public partial class Registration
    {
        public int CustomerID { get; set; }
        public string ProductCode { get; set; }
        public System.DateTime RegistrationDate { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}
