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
    public class ClearanceController : Controller
    {
        private ApplicationDbContext context;

        public ClearanceController()
        {
            this.context = new ApplicationDbContext();
        }

        // GET: Clearance
        [Authorize(Roles="DaytimeCoordinator")]
        public ActionResult Requests()
        {
            IEnumerable<Clearance> Requests = context.Clearances.Where(m=>m.Cleared == null).OrderBy(m=>m.RequestDate).ToList();
            return View(Requests);
        }

        // GET: Clearance/Request
        [Authorize(Roles="RegularStudent")]
        public ActionResult Request()
        {
            string studentId = User.Identity.GetUserId();
            DateTime timeLimit = DateTime.Now.AddDays(-15);
            RequestClearanceViewModel model = new RequestClearanceViewModel();
            model.Reasons = context.ClearanceReasons.ToList();
            model.PreviousClearances = context.Clearances.Include("Student").Include("Reason").Where(m=>m.StudentId == studentId).Where(m=>m.RequestDate > timeLimit).Where(m=>m.Cleared != false).ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "RegularStudent")]
        public ActionResult Request(RequestClearanceViewModel model)
        {
            IEnumerable<ClearanceReason> Reasons = context.ClearanceReasons.ToList();

            if(ModelState.IsValid)
            {
                Student student = (Student)context.Users.Find(User.Identity.GetUserId());
                Clearance clearance = new Clearance
                {
                    ClearanceReasonId = model.ClearanceReasonId,
                    Remark = model.Remark,
                    StudentId = student.Id,
                    RequestDate = DateTime.Now
                };
                context.Clearances.Add(clearance);
                context.SaveChanges();

                // Successful
                TempData["SuccessMessage"] = "You have made a clearance request.";
            }
            model.Reasons = Reasons;
            return RedirectToAction("StudentDashboard", "Home", null);
        }

        [Authorize(Roles = "DaytimeCoordinator")]
        public ActionResult Details(int id)
        {
            ClearanceDetailsViewModel model = new ClearanceDetailsViewModel();
            Clearance clearance = context.Clearances.Include("Student").Include("Reason").Single(m=>m.ClearanceId == id);
            LibraryClearance libraryClearance = context.LibraryClearances.Include("Librarian").SingleOrDefault(m => m.ClearanceId == id);
            PropertyClearance propertyClearance = context.PropertyClearances.Include("Property").SingleOrDefault(m => m.ClearanceId == id);
            AcademicDeanClearance academicDeanClearance = context.AcademicDeanClearances.Include("AcademicDean").SingleOrDefault(m => m.ClearanceId == id);
            RegistrarClearance registrarClearance = context.RegistrarClearances.Include("Registrar").SingleOrDefault(m => m.ClearanceId == id);


            model.Clearance = clearance;
            model.LibraryClearance = libraryClearance;
            model.PropertyClearance = propertyClearance;
            model.AcademicDeanClearance = academicDeanClearance;
            model.RegistrarClearance = registrarClearance;

            return View(model);
        }

        [Authorize(Roles = "DaytimeCoordinator")]
        [ValidateAntiForgeryToken]
        public ActionResult DenyClearance(int id)
        {
            try
            {
                Clearance clearance = context.Clearances.Find(id);
                clearance.Cleared = false;
                context.Entry(clearance).State = EntityState.Modified;
                context.SaveChanges();

                TempData["SuccessMessage"] = "The clearance request has been denied.";
                return RedirectToAction("Requests");

            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return Redirect(base.Request.UrlReferrer.ToString());
        }

        [Authorize(Roles = "DaytimeCoordinator")]
        [ValidateAntiForgeryToken]
        public ActionResult ClearStudent(int id)
        {
            try
            {
                Clearance clearance = context.Clearances.Find(id);
                clearance.Cleared = true;
                context.Entry(clearance).State = EntityState.Modified;
                context.SaveChanges();

                TempData["SuccessMessage"] = "The student has been cleared.";
                return RedirectToAction("Requests");

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return Redirect(base.Request.UrlReferrer.ToString());
        }

        public ActionResult DeleteRequest(int id)
        {
            Clearance clearance = context.Clearances.Find(id);
            LibraryClearance libraryClearance = context.LibraryClearances.SingleOrDefault(m => m.ClearanceId == id);
            PropertyClearance propertyClearance = context.PropertyClearances.SingleOrDefault(m => m.ClearanceId == id);
            AcademicDeanClearance academicDeanClearance = context.AcademicDeanClearances.SingleOrDefault(m => m.ClearanceId == id);
            RegistrarClearance registrarClearance = context.RegistrarClearances.SingleOrDefault(m => m.ClearanceId == id);

            context.Clearances.Remove(clearance);

            if ((context.PropertyClearances != null) && (propertyClearance != null))
            {
                context.PropertyClearances.Remove(propertyClearance);

            }
            if ((context.AcademicDeanClearances != null) && (academicDeanClearance != null))
            {
                context.AcademicDeanClearances.Remove(academicDeanClearance);

            }

            if ((context.RegistrarClearances != null) && (registrarClearance != null))
            {
                context.RegistrarClearances.Remove(registrarClearance);

            }

            if ((context.LibraryClearances != null) && (libraryClearance != null))
            {
                context.LibraryClearances.Remove(libraryClearance);

            }

            context.SaveChanges();

            return RedirectToAction("Request");
        }
    }
}