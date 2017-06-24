using SIMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace SIMS.Controllers
{
    public class ClearanceController : Controller
    {
        public ActionResult Index()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            IEnumerable<Clearance> clearances = context.Clearances.Include("Student");
            return View(clearances);
        }

        // GET: Clearance
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Clearance clearance)
        {
            if(ModelState.IsValid)
            {
                clearance.StudentId = User.Identity.GetUserId();
            }

            ApplicationDbContext context = new ApplicationDbContext();
            context.Clearances.Add(clearance);
            context.SaveChanges();

            TempData["message"] = "Your clearance request has been submitted";
            return View(clearance);
        }
    }
}