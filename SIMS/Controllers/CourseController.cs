using SIMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace SIMS.Controllers
{
    public class CourseController : Controller
    {
        private ApplicationDbContext context;

        public CourseController()
        {
            context = new ApplicationDbContext();
        }
        
        // GET: Course/Register
        [Authorize(Roles="Student")]
        public ActionResult Register()
        {
            // The logged in student
            Student student = (Student)context.Users.Find(User.Identity.GetUserId());
            string year = DateTime.Now.Year.ToString(); 
            int semester = DateTime.Now.Month < 7 ? 1 : 2;

            IEnumerable<StudentCourse> studentCourses = context.StudentCourses.Where(c => c.StudentId == student.Id).ToList();

            if(studentCourses.Any(c =>c.StudentId == student.Id && c.Year == year && c.Semester == semester))
            {
                TempData["InfoMessage"] = "You have already registered courses for this semester.";
                return RedirectToAction("StudentDashboard", "Home", null);
            }
            else
            {
                IEnumerable<Course> courses = context.Curriculum.Where(c=>c.Year == student.Year).Where(c=>c.Semester == semester).Where(c=>c.Program.ProgramId == student.ProgramId).Where(c=>c.LevelId == student.LevelId).ToList();

                RegisterCourseViewModel model = new RegisterCourseViewModel();
                model.Courses = courses;
                return View(model);
            }

        }

        // POST: Course/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Student")]
        public ActionResult Register(RegisterCourseViewModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    Student student = (Student)context.Users.Find(User.Identity.GetUserId());
                    foreach(int courseId in model.SelectedCourses)
                    {
                        StudentCourse sc = new StudentCourse
                        {
                            CourseId = courseId,
                            StudentId = student.Id,
                            Year = DateTime.Now.Year.ToString(),
                            Semester = DateTime.Now.Month < 7 ? 1 : 2
                        };
                        context.StudentCourses.Add(sc);
                    }
                    context.SaveChanges();
                    TempData["SuccessMessage"] = "You have been enrolled in the courses.";
                }
                catch(Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                }
                
            }
            return RedirectToAction("StudentDashboard","Home",null);
        }
        
        // GET: Course/Students
        // Displays a list of students taking a particular course
        [Authorize(Roles = "Teacher")]
        public ActionResult Students()
        {
            string year = DateTime.Now.Year.ToString();
            // The logged in teacher
            Teacher teacher = (Teacher)context.Users.Find(User.Identity.GetUserId());
            IEnumerable<TeacherCourse> teacherCouses = context.TeacherCourses.Where(m => m.TeacherId == teacher.Id).Where(m=>m.Year == year).ToList();

            StudentsListViewModel model = new StudentsListViewModel();
            model.Courses = teacherCouses.Select(m => m.Course).ToList();
            return View(model);
        }

        // POST: Course/Students
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult Students(StudentsListViewModel model)
        {
            // The logged in teacher
            Teacher teacher = (Teacher)context.Users.Find(User.Identity.GetUserId());
            IEnumerable<TeacherCourse> teacherCouses = context.TeacherCourses.Where(m => m.TeacherId == teacher.Id).ToList();

            if(ModelState.IsValid && teacherCouses.Any(m=>m.CourseId == model.CourseId))
            {
                model.Students = context.StudentCourses.Where(m => m.CourseId == model.CourseId).ToList();
            }
            model.Courses = teacherCouses.Select(m => m.Course).ToList();
            return View(model);
        }

        // GET: Course/AssignTeacher
        [Authorize(Roles = "AcademicDean")]
        public ActionResult Teachers()
        {
            AssignTeacherViewModel model = new AssignTeacherViewModel();            
            IEnumerable<TeacherCourse> teacherCourses = context.TeacherCourses.ToList();
            IEnumerable<Teacher> teachers = context.Teachers.ToList();
            IEnumerable<Course> courses = context.Curriculum.ToList();

            model.TeacherCourses = teacherCourses;
            model.Teachers = teachers;
            model.Courses = courses;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "AcademicDean")]
        public ActionResult AssignTeacher(AssignTeacherViewModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    TeacherCourse tc = new TeacherCourse
                    {
                        TeacherId = model.TeacherId,
                        CourseId = model.CourseId,
                        Year = DateTime.Now.Year.ToString()
                    };
                    context.TeacherCourses.Add(tc);
                    context.SaveChanges();

                    TempData["SuccessMessage"] = "Teacher has been assigned.";
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;   
                }
            }

            return RedirectToAction("Teachers");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "AcademicDean")]
        public ActionResult UnassignTeacher(int TeacherCourseId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    TeacherCourse tc = context.TeacherCourses.Where(m => m.TeacherCourseId == TeacherCourseId).SingleOrDefault();
                    context.TeacherCourses.Remove(tc);
                    context.SaveChanges();

                    TempData["SuccessMessage"] = "Teacher has been unassigned.";
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                }
            }


            return RedirectToAction("Teachers"); ;
        }

        [Authorize(Roles = "Student")]
        public ActionResult AddDrop()
        {
            Student student = (Student)context.Users.Find(User.Identity.GetUserId());
            string year = DateTime.Now.Year.ToString();
            int semester = DateTime.Now.Month < 7 ? 1 : 2;

            AddDropCourseViewModel model = new AddDropCourseViewModel();
            IEnumerable<StudentCourse> currentCourses = context.StudentCourses.Include("Student").Include("Course").Where(m=>m.StudentId == student.Id).Where(m=>m.Year == year).Where(m=>m.Semester == semester).ToList();

            IEnumerable<StudentCourse> pastYearsCourses = context.StudentCourses.Include("Student").Include("Course").Where(m => m.StudentId == student.Id).Where(m => m.Year != year).ToList();
            IEnumerable<StudentCourse> pastSemester = context.StudentCourses.Include("Student").Include("Course").Where(m => m.StudentId == student.Id).Where(m => m.Year == year).Where(m => m.Semester <= semester).ToList();

            IEnumerable<StudentCourse> availableCourses = pastYearsCourses.Concat(pastSemester).ToList();
            model.AvailableCourses = availableCourses.Except(currentCourses, new StudentCourseComparer()).ToList();
            model.CurrentCourses = currentCourses;

            return View(model);
        }

        public class StudentCourseComparer : IEqualityComparer<StudentCourse>
        {
            public bool Equals(StudentCourse x, StudentCourse y)
            {
                if(x==null && y==null)
                {
                    return true;
                }
                else if(x==null || y==null)
                {
                    return false;
                }
                else if (x.CourseId == y.CourseId)
                {
                    return true;
                }
                else
                {
                    return false;                
                }
            }

            public int GetHashCode(StudentCourse obj)
            {
                return obj.CourseId.GetHashCode();
            }                       
        }

        public ActionResult Drop(AddDropCourseViewModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    StudentCourse sc = context.StudentCourses.Find(model.StudentCourseId);
                    context.StudentCourses.Remove(sc);
                    context.SaveChanges();

                    TempData["SuccessMessage"] = "You have droped the course.";
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                }
            }
            return RedirectToAction("AddDrop");
        }


        public ActionResult Add(AddDropCourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string studentId = User.Identity.GetUserId();                    
                    StudentCourse sc = new StudentCourse
                    {
                        CourseId = model.CourseId,
                        StudentId = studentId,
                        Year = DateTime.Now.Year.ToString(),
                        Semester = DateTime.Now.Month < 7 ? 1 : 2
                    };
                    context.StudentCourses.Add(sc);
                    context.SaveChanges();
                    TempData["SuccessMessage"] = "You have been enrolled in the courses.";
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                }

            }
            return RedirectToAction("AddDrop");
        }
    }
}