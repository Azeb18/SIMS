using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIMS.Models
{
    public class Clearance
    {
        public int ClearanceId { get; set; }
        public string StudentId { get; set; }
        public int ClearanceReasonId { get; set; }
        public string Remark { get; set; }
        public DateTime RequestDate { get; set; }
        public bool? Cleared { get; set; }

        // Navigation Properties
        public virtual Student Student { get; set; }
        public virtual ClearanceReason Reason { get; set; }
    }

    public class ClearanceReason
    {
        public int ClearanceReasonId { get; set; }
        public string Reason { get; set; }
    }


    public class LibraryClearance
    {
        public int LibraryClearanceId { get; set; }
        public int ClearanceId { get; set; }
        public string LibarianId { get; set; }
        public bool? Cleared { get; set; }

        // Navigation Properties
        public virtual Clearance Clearance { get; set; }
        public virtual Librarian Librarian { get; set; }

        // Not mapped properties
        [NotMapped]
        public string Status
        {
            get
            {
                switch(Cleared)
                {
                    case null:
                        return "Request Sent";
                    case true:
                        return "Cleared";
                    case false:
                        return "Denied";
                    default:
                        throw new Exception("Invalid Library Clearance Status!");
                }
            }
        }
    }

    public class PropertyClearance
    {
        public int PropertyClearanceId { get; set; }
        public int ClearanceId { get; set; }
        public string PropertyId { get; set; }
        public bool? Cleared { get; set; }

        // Navigation Properties
        public virtual Clearance Clearance { get; set; }
        public virtual Property Property { get; set; }

        // Not mapped properties
        [NotMapped]
        public string Status
        {
            get
            {
                switch (Cleared)
                {
                    case null:
                        return "Request Sent";
                    case true:
                        return "Cleared";
                    case false:
                        return "Denied";
                    default:
                        throw new Exception("Invalid Property Clearance Status!");
                }
            }
        }
    }

    public class AcademicDeanClearance
    {
        public int AcademicDeanClearanceId { get; set; }
        public int ClearanceId { get; set; }
        public string AcademicDeanId { get; set; }
        public bool? Cleared { get; set; }

        // Navigation Properties
        public virtual Clearance Clearance { get; set; }
        public virtual AcademicDean AcademicDean { get; set; }

        // Not mapped properties
        [NotMapped]
        public string Status
        {
            get
            {
                switch (Cleared)
                {
                    case null:
                        return "Request Sent";
                    case true:
                        return "Cleared";
                    case false:
                        return "Denied";
                    default:
                        throw new Exception("Invalid Academic Dean Clearance Status!");
                }
            }
        }
    }

    public class RegistrarClearance
    {
        public int RegistrarClearanceId { get; set; }
        public int ClearanceId { get; set; }
        public string RegistrarId { get; set; }
        public bool? Cleared { get; set; }

        // Navigation Properties
        public virtual Clearance Clearance { get; set; }
        public virtual Registrar Registrar { get; set; }

        // Not mapped properties
        [NotMapped]
        public string Status
        {
            get
            {
                switch (Cleared)
                {
                    case null:
                        return "Request Sent";
                    case true:
                        return "Cleared";
                    case false:
                        return "Denied";
                    default:
                        throw new Exception("Invalid Registrar Clearance Status!");
                }
            }
        }
    }

    // ViewModels
    public class RequestClearanceViewModel
    {
        [Display(Name = "Reason for Clearance")]
        public int ClearanceReasonId { get; set; }
        public string Remark { get; set; }

        //Options
        public IEnumerable<ClearanceReason> Reasons { get; set; }
        public IEnumerable<Clearance> PreviousClearances { get; set; }
    }

    public class ClearanceDetailsViewModel
    {
        public Clearance Clearance { get; set; }
        public LibraryClearance LibraryClearance { get; set; }
        public PropertyClearance PropertyClearance { get; set; }
        public AcademicDeanClearance AcademicDeanClearance { get; set; }
        public RegistrarClearance RegistrarClearance { get; set; }
    }

    public class LibraryClearanceDetailsViewModel
    {
        public LibraryClearance LibraryClearance { get; set; }
        public IEnumerable<Loan> BookLoans { get; set; }
    }
}