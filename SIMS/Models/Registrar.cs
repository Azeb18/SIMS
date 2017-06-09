using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIMS.Models
{
    public class StudentBioViewModel
    {
        [Display(Name="ID Number")] 
        [Required]
        public string StudentIdNumber { get; set; }
        public Student Student { get; set; }
        public IEnumerable<Clearance> Clearances { get; set; }
        public IEnumerable<StudentCourse> CurrentCourses { get; set; }
        public IEnumerable<StudentCourse> Grades { get; set; }
        public IEnumerable<Loan> Loans { get; set; }
    }
}