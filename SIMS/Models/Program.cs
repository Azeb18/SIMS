using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIMS.Models
{
    public class Program
    {
        public int ProgramId { get; set; }
        [Display(Name="Program")]
        public string Name { get; set; }
        public string IdPrefix { get; set; }
    }

    // Regular , Extension , Distance
    // R, Ex , DD
}