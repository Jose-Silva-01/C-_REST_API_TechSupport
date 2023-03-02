using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechSupport.Models;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace TechSupport.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult CustomersDisplay(string searchCustomers, int top = 10, int page = 1)
        {
            int skip = (page - 1) * top;
            var context = new TechSupportEntities();

            List<Customer> desiredCustomers = context.Customers.ToList();
            List < Customer > customersDisplay = context.Customers.OrderBy(c=>c.Name).Skip(skip).Take(top).ToList();
            
            if (!string.IsNullOrWhiteSpace(searchCustomers))
            {
                desiredCustomers = desiredCustomers.Where(c => c.Name.IndexOf(searchCustomers) != -1).ToList();
                customersDisplay = desiredCustomers.OrderBy(c => c.Name).Skip(skip).Take(top).ToList();
            }

            int desiredCustomersQty = desiredCustomers.Count();
            ViewBag.totalCustomers = desiredCustomersQty;
            ViewBag.page = page;
            ViewBag.top = top;
            ViewBag.searchTerm = searchCustomers;
            ViewBag.entity = "Customers";

            return View(customersDisplay);
        }

        [HttpPost]
        public ActionResult AddCustomers(Customer customer)
        {
            var context = new TechSupportEntities();
            string message = "";

            try
            {
                if (string.IsNullOrWhiteSpace(customer.Name) || string.IsNullOrWhiteSpace(customer.Phone) || string.IsNullOrWhiteSpace(customer.Address) || string.IsNullOrWhiteSpace(customer.City) || string.IsNullOrWhiteSpace(customer.State) || string.IsNullOrWhiteSpace(customer.ZipCode) || string.IsNullOrWhiteSpace(customer.Email))
                {
                    message = "Please provide all the required fields";
                }
                else
                {
                    if (context.Customers.Any(c => c.CustomerID == customer.CustomerID))
                        message = "The customer " + customer.Name + " was successfuly updated";
                    else
                        message = "The customer " + customer.Name + " was successfuly added";
                    context.Customers.AddOrUpdate(customer);
                    context.SaveChanges();
                    
                }
            }
            catch (Exception)
            {

                throw;
            }


            AddCustomersList dataList = generateData(customer.CustomerID);

            return new JsonResult()
            {
                Data = new { message },
                JsonRequestBehavior = JsonRequestBehavior.DenyGet
            };

            //return View(dataList);
        }

        public ActionResult AddCustomers(int id = 0)
        {
            AddCustomersList dataList = generateData(id);

            return View(dataList);
        }

        public ActionResult DeleteCustomers(int txtCustomerID)
        {
            var context = new TechSupportEntities();
            Customer deleteCustomer = context.Customers.FirstOrDefault(c => c.CustomerID == txtCustomerID);
            string message = "You can not delete the customer " + deleteCustomer.Name;
            
            return new JsonResult()
            {
                Data = new { message },
                JsonRequestBehavior = JsonRequestBehavior.DenyGet
            };
        }

        private AddCustomersList generateData(int id)
        {
            var context = new TechSupportEntities();

            AddCustomersList dataList = new AddCustomersList
            {
                Customer = context.Customers.FirstOrDefault(c => c.CustomerID == id),
                States = context.States.ToList()
            };

            return dataList;
        }

        //private void sendEmail()
        //{
        //    string senderEmail = ConfigurationManager.AppSettings.Get("SenderEmail");
        //    string senderEmailPassword = ConfigurationManager.AppSettings.Get("SenderEmailPassword");
        //    string emailSMTPServer = ConfigurationManager.AppSettings.Get("EmailSMTPServer");
        //    string emailSMTPServerPort = ConfigurationManager.AppSettings.Get("EmailSMTPServerPort");


        //    var fromAddress = new MailAddress(senderEmail, "Jose");
        //    var toAddress = new MailAddress("zekaaccarlos@gmail.com", "To Name");
        //    string FROM_PASSWORD = senderEmailPassword;
        //    const string subject = "test";
        //    const string body = "Hey now!!";

        //    var smtp = new SmtpClient
        //    {
        //        Host = emailSMTPServer,
        //        Port = Int32.TryParse(emailSMTPServerPort),
        //        EnableSsl = true,
        //        DeliveryMethod = SmtpDeliveryMethod.Network,
        //        Credentials = new NetworkCredential(fromAddress.Address, FROM_PASSWORD),
        //        Timeout = 20000
        //    };
        //    using (var message = new MailMessage(fromAddress, toAddress)
        //    {
        //        Subject = subject,
        //        Body = body
        //    })
        //    {
        //        smtp.Send(message);
        //    }
        //}
    }
}