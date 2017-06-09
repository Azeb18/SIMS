using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIMS.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string EditionYear { get; set; }
        public int Amount { get; set; }
    }

    public class Loan
    {
        public int LoanId { get; set; }
        [Display(Name="Book Id")]
        public int BookId { get; set; }
        public string StudentId { get; set; }
        public string LibrarianId { get; set; }
        [Display(Name = "Loan Date")]
        public DateTime LoanDate { get; set; }
        [Display(Name = "Return Date")]
        public DateTime? ReturnDate { get; set; }
        [DataType(DataType.Currency)]
        public Decimal? Fine { get; set; }

        // Navigation Properties
        public virtual Book Book { get; set; }
        public virtual Student Student { get; set; }
        public virtual Librarian Librarian { get; set; }
    }

    public class MakeLoanViewModel
    {
        [Required]
        [Display(Name = "Book Id")]
        public int BookId { get; set; }
        [Required]
        [Display(Name = "Student Id Number")]
        public string StudentIdNumber { get; set; }

        // options
        public IEnumerable<Book> Books { get; set; }
    }
}