using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechSupport.Models;

namespace TechSupport.Controllers
{
    public class IncidentsController : Controller
    {
        // GET: Incidents
        public ActionResult IncidentsDisplay(string searchIncidents, int top = 10, int page = 1)
        {
            int skip = (page - 1) * top;

            var context = new TechSupportEntities();

            List<Incident> desiredIncidents = context.Incidents.ToList();
            List<Incident> incidentsDisplay = context.Incidents.OrderBy(i => i.IncidentID).Skip(skip).Take(top).ToList();

            if (!string.IsNullOrWhiteSpace(searchIncidents))
            {
                searchIncidents = searchIncidents.ToLower();
                desiredIncidents = desiredIncidents.Where(i => i.Title.ToLower().IndexOf(searchIncidents) != -1).ToList();
                incidentsDisplay = desiredIncidents.OrderBy(i => i.IncidentID).Skip(skip).Take(top).ToList();
            }

            ViewBag.totalIncidents = desiredIncidents.Count();
            ViewBag.page = page;
            ViewBag.top = top;
            ViewBag.searchTerm = searchIncidents;
            ViewBag.entity = "Incidents";
            return View(incidentsDisplay);
        }

        public ActionResult AddIncidents(int id = 0)
        {
            AddIncidentList data = generateData(id);

            return View(data);
        }

        [HttpPost]
        public ActionResult AddIncidents(AddIncidentList data)
        {
            var context = new TechSupportEntities();
            string message = "";

            try
            {
                if (string.IsNullOrWhiteSpace(data.Incident.Title) || string.IsNullOrWhiteSpace(data.Incident.Description))
                    message = "Please proivde all the required fields.";
                else
                {
                    if (context.Incidents.Any(i => i.IncidentID == data.Incident.IncidentID))
                        message = "The incident " + data.Incident.Title + " has been updated.";
                    else
                        message = "The incident " + data.Incident.Title + " has been created.";

                    context.Incidents.AddOrUpdate(data.Incident);
                    context.SaveChanges();
                }
                
            }catch(Exception ex)
            {
                throw;
            }

            AddIncidentList returnData = generateData(data.Incident.IncidentID);
            return new JsonResult()
            {
                Data = new { message },
                JsonRequestBehavior = JsonRequestBehavior.DenyGet
            };
        }

        public ActionResult DeleteIncidents(int txtIncidentID = 0)
        {
            var context = new TechSupportEntities();
            Incident deleteIncident = context.Incidents.FirstOrDefault(i => i.IncidentID == txtIncidentID);
            string message = "";
            try
            {
                context.Incidents.Remove(deleteIncident);
                context.SaveChanges();
                message = "The Incident " + deleteIncident.Title + " with ID: " + deleteIncident.IncidentID + " was deleted.";
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

        private AddIncidentList generateData(int id = 0)
        {
            var context = new TechSupportEntities();
            AddIncidentList data;

            if (id == 0)
            {
                data = new AddIncidentList
                {
                    Incident = new Incident { IncidentID = id },
                    Customers = context.Customers.ToList(),
                    Products = context.Products.ToList(),
                    Technicians = context.Technicians.ToList()
                };
            }
            else
            {
                data = new AddIncidentList
                {
                    Incident = context.Incidents.FirstOrDefault(i=>i.IncidentID == id),
                    Customers = context.Customers.ToList(),
                    Products = context.Products.ToList(),
                    Technicians = context.Technicians.ToList()
                };
            }

            return data;
        }
    }
}