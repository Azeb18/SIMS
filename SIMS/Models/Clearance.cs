using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIMS.Models
{
    public class Clearance
    {
        public int ClearanceId { get; set; }
        public string StudentId { get; set; }
        public Reason Reason { get; set; }
        public string Remark { get; set; }

        //Navigation Properties
        public Student Student { get; set; }
    }

    public enum Reason
    {
        IdReplacement, EndOfClass
    }
}