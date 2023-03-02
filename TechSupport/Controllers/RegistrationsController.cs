using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechSupport.Models;

namespace TechSupport.Controllers
{
    public class RegistrationsController : Controller
    {
        // GET: Registrations
        public ActionResult RegistrationsDisplay(string searchRegistrations, int top = 7, int page = 1)
        {
            int skip = (page - 1) * top;

            var context = new TechSupportEntities();

            List<Registration> desiredRegistrations = context.Registrations.ToList();
            List<Registration> registrationsDisplay = context.Registrations.OrderBy(r => r.RegistrationDate).Skip(skip).Take(top).ToList();

            if (!string.IsNullOrWhiteSpace(searchRegistrations))
            {
                searchRegistrations = searchRegistrations.ToLower();

                desiredRegistrations = desiredRegistrations.Where(r => r.Customer.Name.ToLower().IndexOf(searchRegistrations) != -1).ToList();
                registrationsDisplay = desiredRegistrations.OrderBy(r => r.RegistrationDate).Skip(skip).Take(top).ToList();
            }

            ViewBag.totalRegistrations = desiredRegistrations.Count();
            ViewBag.page = page;
            ViewBag.top = top;
            ViewBag.searchTerm = searchRegistrations;
            ViewBag.entity = "Registrations";

            return View(registrationsDisplay);
        }

        public ActionResult AddRegistrations(int id = 0, string code="")
        {
            AddRegistrationList data = generateData(id, code);

            return View(data);
        }

        [HttpPost]
        public ActionResult AddRegistrations(AddRegistrationList data)
        {
            var context = new TechSupportEntities();
            string message = "";

            try
            {
                if (context.Registrations.Any(r => r.CustomerID == data.Registration.CustomerID && r.ProductCode == data.Registration.ProductCode))
                    message = "Registration of product " + data.Registration.ProductCode.Trim() + " and customer " + data.Registration.CustomerID + " has been updated.";
                else
                {
                    message = "Registration of product " + data.Registration.ProductCode.Trim() + " and customer " + data.Registration.CustomerID + " has been created.";
                }
                context.Registrations.AddOrUpdate(data.Registration);
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
            //AddRegistrationList returnData = generateData(data.Registration.CustomerID, data.Registration.ProductCode);

            return new JsonResult()
            {
                Data = new { message },
                JsonRequestBehavior = JsonRequestBehavior.DenyGet
            };
        }

        public ActionResult DeleteRegistrations(int txtRegistrationCustomerID = 0, string txtRegistrationProductCode = "")
        {
            var context = new TechSupportEntities();
            Registration deleteReg = context.Registrations.FirstOrDefault(r => r.CustomerID == txtRegistrationCustomerID && r.ProductCode == txtRegistrationProductCode);
            string message = "";

            try
            {
                context.Registrations.Remove(deleteReg);
                context.SaveChanges();
                message = "Registration of product " + deleteReg.ProductCode.Trim() + " and customer " + deleteReg.CustomerID + " has been deleted.";
            }
            catch (Exception)
            {

                throw;
            }

            return new JsonResult()
            {
                Data = new { message },
                JsonRequestBehavior = JsonRequestBehavior.DenyGet
            };
        }

        private AddRegistrationList generateData(int id = 0, string code = "")
        {
            var context = new TechSupportEntities();
            AddRegistrationList data;

            if(id == 0 && string.IsNullOrEmpty(code))
            {
                data = new AddRegistrationList
                {
                    Registration = new Registration { CustomerID = id, ProductCode = code },
                    Customers = context.Customers.ToList(),
                    Products = context.Products.ToList()
                };
            }
            else
            {
                data = new AddRegistrationList
                {
                    Registration = context.Registrations.FirstOrDefault(r => r.CustomerID == id && r.ProductCode == code),
                    Customers = context.Customers.ToList(),
                    Products = context.Products.ToList()
                };
            }
            

            return data;
        }
    }
}