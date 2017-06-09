using SIMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIMS.Controllers
{
    public class SectionController : Controller
    {
        private ApplicationDbContext context;

        public SectionController()
        {
            this.context = new ApplicationDbContext();
        }

        // GET: Section
        [Authorize(Roles = "ExtensionAndDistanceCoordinator")]        
        public ActionResult List()
        {
            SectionPlacementViewModel model = new SectionPlacementViewModel();
            model.Programs = context.Programs.ToList();
            model.Levels = context.Levels.ToList();
            return View("Index", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ExtensionAndDistanceCoordinator")]
        public ActionResult List(SectionPlacementViewModel model)
        {
            model.Programs = context.Programs.ToList();
            model.Levels = context.Levels.ToList();

            IEnumerable<Student> students = context.Students.Where(m => m.LevelId == model.LevelId).Where(m => m.ProgramId == model.ProgramId).Where(m => m.Year == model.Year).ToList();
            model.Students = students;
            return View("Index",model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ExtensionAndDistanceCoordinator")]
        public ActionResult AssignSection(SectionPlacementViewModel model)
        {

            return View("Index", model);
        }

    }
}