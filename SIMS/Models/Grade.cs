using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIMS.Models
{
    public class Grade
    {
        public int GradeId { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
    }

    // View models
    public class EditGradeViewModel
    {        
        public StudentCourse StudentCourse { get; set; }
        public int StudentCourseId { get; set; }
        public int Grade { get; set; }

        // Options
        public IEnumerable<Grade> Grades { get; set; }
    }
}