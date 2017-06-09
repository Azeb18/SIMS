using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIMS.Models
{
    public class Course
    {
        [Display(Name="Course")]
        public int CourseId { get; set; }
        [StringLength(20)]
        [Index(IsUnique = true)]
        [Display(Name="Course Number")]
        public string CourseNumber { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public int Semester { get; set; }
        [Display(Name = "Credit Hours")]
        public int CreditHours { get; set; }
        public int LevelId { get; set; }
        public int ProgramId { get; set; }

        // Navigation Properties
        public virtual Level Level { get; set; }
        public virtual Program Program { get; set; }
    }

    public class StudentCourse
    {
        public int StudentCourseId { get; set; }
        [Required]
        public string StudentId { get; set; }
        [Required]
        public int CourseId { get; set; }
        public int? GradeId { get; set; }
        [Required]
        public string Year { get; set; }
        [Required]
        public int Semester { get; set; }
        

        //Navigation Properties
        public virtual Grade Grade { get; set; }
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
    }

    public class TeacherCourse
    {
        public int TeacherCourseId { get; set; }
        [Required]
        public string TeacherId { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public string Year { get; set; }

        // Navigation Property
        public virtual Teacher Teacher { get; set; }
        public virtual Course Course { get; set; }
    }


    // View Models

    public class RegisterCourseViewModel
    {
        public int[] SelectedCourses { get; set; }
        // Options
        public IEnumerable<Course> Courses { get; set; }
    }

    public class StudentsListViewModel
    {
       [Display(Name = "Course")]
        public int CourseId { get; set; }

        public IEnumerable<StudentCourse> Students { get; set; }       

        // Options
        public IEnumerable<Course> Courses { get; set; }
    }

    public class AssignTeacherViewModel
    {
        public IEnumerable<TeacherCourse> TeacherCourses { get; set; }
        public string TeacherId { get; set; }
        public int CourseId { get; set; }

        // Options
        public IEnumerable<Teacher> Teachers { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }

    public class AddDropCourseViewModel
    {
        public int CourseId { get; set; }
        public int StudentCourseId { get; set; }

        // Options
        public IEnumerable<StudentCourse> CurrentCourses { get; set; }
        public IEnumerable<StudentCourse> AvailableCourses { get; set; }
    }
}