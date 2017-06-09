using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIMS.Models
{
    public class Level
    {
        public int LevelId { get; set; }
        [Display(Name = "Level")]
        public string Name { get; set; }
    }

    // Bachelor , Master
}