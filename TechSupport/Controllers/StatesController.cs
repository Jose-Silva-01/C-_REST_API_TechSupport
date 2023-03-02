using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechSupport.Models;

namespace TechSupport.Controllers
{
    public class StatesController : Controller
    {
        // GET: States
        public ActionResult StatesDisplay(string searchStates, int top = 7, int page = 1)
        {
            int skip = (page - 1) * top;

            var context = new TechSupportEntities();

            List<State> desiredStates = context.States.ToList();
            List<State> statesDisplay = context.States.OrderBy(s => s.StateName).Skip(skip).Take(top).ToList();

            if (!string.IsNullOrWhiteSpace(searchStates))
            {
                desiredStates = desiredStates.Where(s => s.StateCode.IndexOf(searchStates.ToUpper()) != -1).ToList();
                statesDisplay = desiredStates.OrderBy(s => s.StateName).Skip(skip).Take(top).ToList();
            }

            ViewBag.totalStates = desiredStates.Count();
            ViewBag.page = page;
            ViewBag.top = top;
            ViewBag.searchTerm = searchStates;
            ViewBag.entity = "States";

            return View(statesDisplay);
        }

        public ActionResult AddStates(string id = "")
        {
            var context = new TechSupportEntities();

            State state = context.States.FirstOrDefault(s => s.StateCode == id);

            return View(state);
        }

        [HttpPost]
        public ActionResult AddStates(State state)
        {
            var context = new TechSupportEntities();
            string message = "";
            try
            {
                if (string.IsNullOrWhiteSpace(state.StateCode) || string.IsNullOrWhiteSpace(state.StateName) || state.FirstZipCode == 0 || state.LastZipCode == 0)
                    message = "Please provide the required fields.";
                else 
                {
                    if (context.States.Any(s => s.StateCode == state.StateCode))
                        message = "The state " + state.StateName + " has been updated.";
                    else
                        message = "The state " + state.StateName + " has been created.";
                    
                    context.States.AddOrUpdate(state);
                    context.SaveChanges();
                }
                
            }
            catch (Exception ex)
            {
                throw;
            }

            return new JsonResult()
            {
                Data = new { message },
                JsonRequestBehavior = JsonRequestBehavior.DenyGet
            };
        }

        public ActionResult DeleteStates(string txtStateCode = "")
        {
            var context = new TechSupportEntities();
            State state = context.States.FirstOrDefault(s => s.StateCode == txtStateCode);
            string message = "";
            try
            {
                context.States.Remove(state);
                context.SaveChanges();
                message = "The state " + state.StateName + "has been deleted.";
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
    }
}