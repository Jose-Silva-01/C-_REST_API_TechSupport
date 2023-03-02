using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechSupport.Models;

namespace TechSupport.Controllers
{
    public class TechniciansController : Controller
    {
        // GET: Technicians
        public ActionResult TechniciansDisplay(string searchTechnicians, int top = 10, int page = 1)
        {
            int skip = (page - 1) * top;

            var context = new TechSupportEntities();

            List<Technician> desiredTechs = context.Technicians.ToList();
            List<Technician> techsDisplay = context.Technicians.OrderBy(t => t.Name).Skip(skip).Take(top).ToList();

            if (!string.IsNullOrWhiteSpace(searchTechnicians))
            {
                desiredTechs = desiredTechs.Where(t => t.Name.ToLower().IndexOf(searchTechnicians.ToLower()) != -1).ToList();

                techsDisplay = desiredTechs.OrderBy(t => t.Name).Skip(skip).Take(top).ToList();
            }
            
            ViewBag.totalTechs = desiredTechs.Count();
            ViewBag.page = page;
            ViewBag.top = top;
            ViewBag.searchTerm = searchTechnicians;
            ViewBag.entity = "Technicians";

            return View(techsDisplay);
        }

        public ActionResult AddTechnicians(int id = 0)
        {
            var context = new TechSupportEntities();

            Technician tec = context.Technicians.FirstOrDefault(t => t.TechID == id);

            return View(tec);
        }

        [HttpPost]
        public ActionResult AddTechnicians(Technician technician)
        {
            var context = new TechSupportEntities();
            string message = "";
            try
            {
                if (string.IsNullOrWhiteSpace(technician.Name) || string.IsNullOrWhiteSpace(technician.Phone) || string.IsNullOrWhiteSpace(technician.Email))
                    message = "Please provide all the required fields.";
                else
                {
                    if (context.Technicians.Any(t => t.TechID == technician.TechID))
                        message = "The technician " + technician.Name + " was updated";
                    else
                        message = "The technician " + technician.Name + " was created";

                    context.Technicians.AddOrUpdate(technician);
                    context.SaveChanges();
                }

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

        public ActionResult DeleteTechnicians(int txtTechnicianID = 0)
        {
            var context = new TechSupportEntities();

            Technician tec = context.Technicians.FirstOrDefault(t => t.TechID == txtTechnicianID);
            string message = "";
            try
            {
                context.Technicians.Remove(tec);
                context.SaveChanges();
                message = "Technician " + tec.TechID + " deleted successfuly";
            }
            catch(Exception)
            {
                throw;
            }

            return new JsonResult()
            {
                Data = new { message },
                JsonRequestBehavior = JsonRequestBehavior.DenyGet
            };
        }
    }
}