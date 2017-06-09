using SIMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIMS.Controllers
{
    public class CurriculumController : Controller
    {
        private ApplicationDbContext context;
        public CurriculumController()
        {
            context = new ApplicationDbContext();
        }
        // GET: Curriculum
        public ActionResult Index()
        {
            IEnumerable<Course> courses = context.Curriculum.ToList();
            return View(courses);
        }
    }
}