using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIMS.Models
{
    // View Models
    public class SectionPlacementViewModel
    {
        [Display(Name="Level")]
        public int LevelId { get; set; }
        [Display(Name = "Program")]
        public int ProgramId { get; set; }
        public int Year { get; set; }
        [Display(Name = "Number of Sections")]
        public int NumberOfSections { get; set; }

        public IEnumerable<Student> Students { get; set; }

        // Options
        public IEnumerable<Level> Levels { get; set; }
        public IEnumerable<Program> Programs { get; set; }
    }
}