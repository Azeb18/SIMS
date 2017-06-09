using SIMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;

namespace SIMS.Controllers
{
    public class RegistrarController : Controller
    {
        private ApplicationDbContext context;

        public RegistrarController()
        {
            this.context = new ApplicationDbContext();
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Registrar")]
        public ActionResult AddClearanceRequest(int id)
        {
            if (ModelState.IsValid)
            {
                RegistrarClearance registrarRequest = new RegistrarClearance
                {
                    ClearanceId = id
                };

                try
                {

                    context.RegistrarClearances.Add(registrarRequest);
                    context.SaveChanges();

                    TempData["SuccessMessage"] = "Request forwarded to registrar!";
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
        [Authorize(Roles = "Registrar")]
        public ActionResult CancelClearanceRequest(int id)
        {
            try
            {
                RegistrarClearance registrarClearance = context.RegistrarClearances.Find(id);
                context.RegistrarClearances.Remove(registrarClearance);
                context.SaveChanges();

                TempData["SuccessMessage"] = "Registrar clearance request has been cancelled.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return Redirect(base.Request.UrlReferrer.ToString());
        }

        [Authorize(Roles = "Registrar")]
        public ActionResult Clearances()
        {
            IEnumerable<RegistrarClearance> registrarClearances = context.RegistrarClearances.Include("Clearance").Include("Registrar").Where(m => m.Cleared == null).Where(m => m.Clearance.Cleared == null).ToList();
            return View(registrarClearances);
        }

        [Authorize(Roles = "Registrar")]
        public ActionResult Clearance(int id)
        {
            RegistrarClearance registrarClearance = context.RegistrarClearances.Include("Clearance").Include("Registrar").Single(lc => lc.RegistrarClearanceId == id);
            return View(registrarClearance);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Registrar")]
        public ActionResult DenyClearance(int id)
        {
            try
            {
                RegistrarClearance registrarClearance = context.RegistrarClearances.Find(id);
                registrarClearance.Cleared = false;
                registrarClearance.RegistrarId = User.Identity.GetUserId();
                context.Entry(registrarClearance).State = EntityState.Modified;

                Clearance clearance = context.Clearances.Find(registrarClearance.ClearanceId);
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
        [Authorize(Roles = "Registrar")]
        public ActionResult ClearStudent(int id)
        {
            try
            {
                RegistrarClearance registrarClearance = context.RegistrarClearances.Find(id);
                registrarClearance.Cleared = true;
                registrarClearance.RegistrarId = User.Identity.GetUserId();
                context.Entry(registrarClearance).State = EntityState.Modified;

                Clearance clearance = context.Clearances.Find(registrarClearance.ClearanceId);
                clearance.Cleared = true;
                context.Entry(clearance).State = EntityState.Modified;

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

        [Authorize(Roles = "Registrar")]
        public ActionResult StudentBio()
        {
            StudentBioViewModel model = new StudentBioViewModel();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Registrar")]
        [ValidateAntiForgeryToken]
        public ActionResult StudentBio(StudentBioViewModel model)
        {
            if(ModelState.IsValid)
            {
                try 
	            {
                    DateTime timeLimit = DateTime.Now.AddDays(-15);
                    string year = DateTime.Now.Year.ToString();
                    int semester = DateTime.Now.Month < 7 ? 1 : 2;

                    model.Student = context.Students.First(m => m.IdNumber == model.StudentIdNumber);
                    model.Clearances = context.Clearances.Include("Reason").Where(m => m.Student.Id == model.Student.Id).Where(m => m.RequestDate > timeLimit).ToList();

                    model.CurrentCourses = context.StudentCourses.Include("Student").Include("Course").Where(m => m.StudentId == model.Student.Id).Where(m => m.Year == year).Where(m => m.Semester == semester).ToList();

                    model.Loans = context.Loans.Where(m=>m.StudentId == model.Student.Id).Where(m => m.ReturnDate == null).ToList();

                    model.Grades = context.StudentCourses.Include("Student").Include("Course").Include("Grade").Where(c => c.StudentId == model.Student.Id).ToList();

	            }
	            catch (Exception)
	            {
                    ModelState.AddModelError(String.Empty, "There is no student with that ID number");		        
	            }
            }                        
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Registrar")]
        [ValidateAntiForgeryToken]
        public ActionResult ResetStudentPassword(StudentBioViewModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    string password = "Password" + new Random().Next(1000, 9999).ToString() + "-";
                    string passwordHash = new PasswordHasher().HashPassword(password);
                    Student student = context.Students.First(m=>m.IdNumber == model.StudentIdNumber);
                    student.PasswordHash = passwordHash;
                    context.Entry(student).State = EntityState.Modified;
                    context.SaveChanges();

                    TempData["SuccessMessage"] = "Password Reset, the new password is " + password;
                }
                catch (Exception)
                {                    
                }
            }
            return RedirectToAction("StudentBio");
        }
    }
}