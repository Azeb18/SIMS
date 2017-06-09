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
    public class ResultController : Controller
    {
        private ApplicationDbContext context;

        public ResultController()
        {
            this.context = new ApplicationDbContext();
        }

        // GET: Result/GradeReport
        [Authorize(Roles="Student")]
        public ActionResult GradeReport()
        {
            // The logged in student
            Student student = (Student)context.Users.Find(User.Identity.GetUserId());

            IEnumerable<StudentCourse> studentCourses = context.StudentCourses.Include("Student").Include("Course").Include("Grade").Where(c => c.StudentId == student.Id).ToList();

            return View(studentCourses);
        }

        // GET: Result/Edit
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(int id)
        {
            EditGradeViewModel model = new EditGradeViewModel();
            StudentCourse sc = context.StudentCourses.Find(id);
            IEnumerable<Grade> grades = context.Grades.ToList();

            model.StudentCourse = sc;
            model.Grades = grades;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(EditGradeViewModel model)
        {
            try
            {
                StudentCourse sc = context.StudentCourses.Find(model.StudentCourseId);
                sc.GradeId = model.Grade;
                context.Entry(sc).State = EntityState.Modified;
                context.SaveChanges();

                TempData["SuccessMessage"] = "Grade has been updated!";
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction("Students","Course",null);
        }
    }
}