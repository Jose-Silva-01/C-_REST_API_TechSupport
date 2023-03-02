using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechSupport.Models
{
    public class AddRegistrationList
    {
        public Registration Registration { get; set; }

        public List<Customer> Customers { get; set; }

        public List<Product> Products { get; set; }
    }
}