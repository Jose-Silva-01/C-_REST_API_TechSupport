using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechSupport.Models
{
    public class AddCustomersList
    {
        public Customer Customer { get; set; }

        public List<State> States { get; set; }
    }
}