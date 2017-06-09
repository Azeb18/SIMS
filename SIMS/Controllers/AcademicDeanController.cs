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
    public class AcademicDeanController : Controller
    {
        private ApplicationDbContext context;

        public AcademicDeanController()
        {
            this.context = new ApplicationDbContext();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="AcademicDean")]
        public ActionResult AddClearanceRequest(int id)
        {
            if (ModelState.IsValid)
            {
                AcademicDeanClearance academicDeanRequest = new AcademicDeanClearance
                {
                    ClearanceId = id
                };

                try
                {

                    context.AcademicDeanClearances.Add(academicDeanRequest);
                    context.SaveChanges();

                    TempData["SuccessMessage"] = "Request forwarded to academic dean!";
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
        [Authorize(Roles = "AcademicDean")]
        public ActionResult CancelClearanceRequest(int id)
        {
            try
            {
                AcademicDeanClearance academicDeanClearance = context.AcademicDeanClearances.Find(id);
                context.AcademicDeanClearances.Remove(academicDeanClearance);
                context.SaveChanges();

                TempData["SuccessMessage"] = "Academic dean clearance request has been cancelled.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return Redirect(base.Request.UrlReferrer.ToString());
        }

        [Authorize(Roles = "AcademicDean")]
        public ActionResult Clearances()
        {
            IEnumerable<AcademicDeanClearance> academicDeanClearances = context.AcademicDeanClearances.Include("Clearance").Include("AcademicDean").Where(m => m.Cleared == null).Where(m => m.Clearance.Cleared == null).ToList();
            return View(academicDeanClearances);
        }

        [Authorize(Roles = "AcademicDean")]
        public ActionResult Clearance(int id)
        {
            AcademicDeanClearance academicDeanClearance = context.AcademicDeanClearances.Include("Clearance").Include("AcademicDean").Single(lc => lc.AcademicDeanClearanceId == id);
            return View(academicDeanClearance);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "AcademicDean")]
        public ActionResult DenyClearance(int id)
        {
            try
            {
                AcademicDeanClearance academicDeanClearance = context.AcademicDeanClearances.Find(id);
                academicDeanClearance.Cleared = false;
                academicDeanClearance.AcademicDeanId = User.Identity.GetUserId();
                context.Entry(academicDeanClearance).State = EntityState.Modified;

                Clearance clearance = context.Clearances.Find(academicDeanClearance.ClearanceId);
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
        [Authorize(Roles = "AcademicDean")]
        public ActionResult ClearStudent(int id)
        {
            try
            {
                AcademicDeanClearance academicDeanClearance = context.AcademicDeanClearances.Find(id);
                academicDeanClearance.Cleared = true;
                academicDeanClearance.AcademicDeanId = User.Identity.GetUserId();
                context.Entry(academicDeanClearance).State = EntityState.Modified;

                RegistrarClearance registrarClearance = new RegistrarClearance
                {
                    ClearanceId = academicDeanClearance.ClearanceId
                };
                context.RegistrarClearances.Add(registrarClearance);

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