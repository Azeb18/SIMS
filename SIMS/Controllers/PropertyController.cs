using SIMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace SIMS.Controllers
{
    public class PropertyController : Controller
    {
        private ApplicationDbContext context;

        public PropertyController()
        {
            this.context = new ApplicationDbContext();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Property")]
        public ActionResult AddClearanceRequest(int id)
        {
            if (ModelState.IsValid)
            {
                PropertyClearance propertyRequest = new PropertyClearance
                {
                    ClearanceId = id
                };

                try
                {

                    context.PropertyClearances.Add(propertyRequest);
                    context.SaveChanges();

                    TempData["SuccessMessage"] = "Request forwarded to property!";
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                }
            }

            return Redirect(base.Request.UrlReferrer.ToString());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Property")]
        public ActionResult CancelClearanceRequest(int id)
        {
            try
            {
                PropertyClearance propertyClearance = context.PropertyClearances.Find(id);
                context.PropertyClearances.Remove(propertyClearance);
                context.SaveChanges();

                TempData["SuccessMessage"] = "Property clearance request has been cancelled.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return Redirect(base.Request.UrlReferrer.ToString());
        }

        [Authorize(Roles = "Property")]
        public ActionResult Clearances()
        {
            IEnumerable<PropertyClearance> propertyClearances = context.PropertyClearances.Include("Clearance").Include("Property").Where(m => m.Cleared == null).Where(m => m.Clearance.Cleared == null).ToList();
            return View(propertyClearances);
        }

        [Authorize(Roles = "Property")]
        public ActionResult Clearance(int id)
        {            
            PropertyClearance propertyClearance = context.PropertyClearances.Include("Clearance").Include("Property").Single(lc => lc.PropertyClearanceId == id);
            return View(propertyClearance);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Property")]
        public ActionResult DenyClearance(int id)
        {
            try
            {
                PropertyClearance propertyClearance = context.PropertyClearances.Find(id);
                propertyClearance.Cleared = false;
                propertyClearance.PropertyId = User.Identity.GetUserId();
                context.Entry(propertyClearance).State = EntityState.Modified;

                Clearance clearance = context.Clearances.Find(propertyClearance.ClearanceId);
                clearance.Cleared = false;
                context.Entry(clearance).State = EntityState.Modified;

                context.SaveChanges();

                // If successful, the clearance is taken care of. so, redirect to list of clearances
                TempData["SuccessMessage"] = "The clearance request has been denied.";
                return RedirectToAction("Clearances");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return Redirect(base.Request.UrlReferrer.ToString());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Property")]
        public ActionResult ClearStudent(int id)
        {
            try
            {
                PropertyClearance propertyClearance = context.PropertyClearances.Find(id);
                propertyClearance.Cleared = true;
                propertyClearance.PropertyId = User.Identity.GetUserId();
                context.Entry(propertyClearance).State = EntityState.Modified;

                AcademicDeanClearance adc = new AcademicDeanClearance
                {
                    ClearanceId = propertyClearance.ClearanceId
                };
                context.AcademicDeanClearances.Add(adc);

                context.SaveChanges();

                // If successful, the clearance is taken care of. so, redirect to list of clearances
                TempData["SuccessMessage"] = "The student has been cleared.";
                return RedirectToAction("Clearances");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return Redirect(base.Request.UrlReferrer.ToString());
        }
    }
}