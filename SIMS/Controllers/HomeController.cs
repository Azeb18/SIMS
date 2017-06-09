using SIMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace SIMS.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext context;

        public HomeController()
        {
            context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            if(!User.Identity.IsAuthenticated)
            {
                return View();
            }
            else if(User.IsInRole("Student"))
            {
                return RedirectToAction("StudentDashboard");
            }
            else if (User.IsInRole("Librarian"))
            {
                return RedirectToAction("Loans","Library",null);
            }
            else if (User.IsInRole("Property"))
            {
                return RedirectToAction("Clearances", "Property", null);
            }
            else if (User.IsInRole("Registrar"))
            {
                return RedirectToAction("Clearances", "Registrar", null);
            }
            else if (User.IsInRole("DaytimeCoordinator"))
            {
                return RedirectToAction("Requests", "Clearance", null);
            }
            else if (User.IsInRole("ExtensionAndDistanceCoordinator"))
            {
                return RedirectToAction("Requests", "Clearance", null);
            }
            else if (User.IsInRole("AcademicDean"))
            {
                return RedirectToAction("Clearances", "AcademicDean", null);
            }
            else if (User.IsInRole("Teacher"))
            {
                return RedirectToAction("Students", "Course", null);
            }
            else if (User.IsInRole("Security"))
            {
                return RedirectToAction("Identification", "Account", null);
            }
            else if (User.IsInRole("Cafe"))
            {
                return RedirectToAction("Identification", "Account", null);
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize(Roles="Student")]
        public ActionResult StudentDashboard()
        {
            Student student = (Student)context.Users.Find(User.Identity.GetUserId());
            return View(student);
        }
    }
}
