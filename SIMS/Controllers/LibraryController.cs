using SIMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace SIMS.Controllers
{
    public class LibraryController : Controller
    {
        private ApplicationDbContext context;

        public LibraryController()
        {
            context = new ApplicationDbContext();
        }

        // GET: Library/Loans
        [Authorize(Roles="Librarian")]
        public ActionResult Loans()
        {
            IEnumerable<Loan> Loans = context.Loans.Where(m=>m.ReturnDate == null).ToList();
            return View(Loans);
        }

        // GET: Library/MakeLoan
        [Authorize(Roles = "Librarian")]
        public ActionResult MakeLoan()
        {
            MakeLoanViewModel model = new MakeLoanViewModel();
            model.Books = context.Books.ToList();
            return View(model);
        }

        // POST: Library/MakeLoan
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Librarian")]
        public ActionResult MakeLoan(MakeLoanViewModel model)
        {
            model.Books = context.Books.ToList();

            if(ModelState.IsValid)
            {
                try
                {
                    Student student = context.Students.First(m => m.IdNumber == model.StudentIdNumber.Trim());                  
                    Book book = context.Books.First(m => m.BookId == model.BookId);   
                    Loan loan = new Loan
                    {
                        BookId = model.BookId,
                        StudentId = student.Id,
                        LibrarianId = User.Identity.GetUserId(),                    
                        LoanDate = DateTime.Now,
                        ReturnDate = null,
                        Fine = null,
                    };

                    context.Loans.Add(loan);
                    context.SaveChanges();
                
                    // Successfull 
                    TempData["BookId"] = model.BookId;
                    TempData["StudentId"] = model.StudentIdNumber;
                }
                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty, "The book/student id you have entered is invalid, please try again.");
                }
                         
            }
            return View(model);
        }

        // GET: Library/Loan
        [Authorize(Roles="Librarian")]
        public ActionResult Loan(int id)
        {
            Loan loan = context.Loans.Include("Book").Include("Student").Single(m => m.LoanId == id);
            return View(loan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Librarian, Coordinator")]
        public ActionResult AddClearanceRequest(int id)
        {
            if (ModelState.IsValid)
            {
                LibraryClearance libraryRequest = new LibraryClearance
                {
                    ClearanceId = id
                };

                try
                {
                    context.LibraryClearances.Add(libraryRequest);
                    context.SaveChanges();

                    TempData["SuccessMessage"] = "Request forwarded to library! Check back later for the reponse.";
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                }
            }

            return Redirect(base.Request.UrlReferrer.ToString());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Librarian, Coordinator")]
        public ActionResult CancelClearanceRequest(int id)
        {
            try
            {
                LibraryClearance libraryClearance = context.LibraryClearances.Find(id);
                context.LibraryClearances.Remove(libraryClearance);
                context.SaveChanges();

                TempData["SuccessMessage"] = "Library clearance request has been cancelled.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return Redirect(base.Request.UrlReferrer.ToString());
        }

        [Authorize(Roles = "Librarian")]
        public ActionResult Clearances()
        {
            IEnumerable<LibraryClearance> libraryClearances = context.LibraryClearances.Include("Clearance").Include("Librarian").Where(m=>m.Cleared == null).Where(m=>m.Clearance.Cleared==null).ToList();
            return View(libraryClearances);
        }

        [Authorize(Roles = "Librarian")]
        public ActionResult Clearance(int id)
        {
            LibraryClearanceDetailsViewModel model = new LibraryClearanceDetailsViewModel();
            LibraryClearance libraryClearance = context.LibraryClearances.Include("Clearance").Include("Librarian").Single(lc => lc.LibraryClearanceId == id);
            IEnumerable<Loan> Loans = context.Loans.Include("Book").Include("Librarian").Where(m => m.StudentId == libraryClearance.Clearance.StudentId).Where(m=>m.ReturnDate == null).ToList();

            model.LibraryClearance = libraryClearance;
            model.BookLoans = Loans;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Librarian")]
        public ActionResult DenyClearance(int id)
        {
            try
            {
                LibraryClearance libraryClearance = context.LibraryClearances.Find(id);
                libraryClearance.Cleared = false;
                libraryClearance.LibarianId = context.Librarians.FirstOrDefault().Id;
                context.Entry(libraryClearance).State = EntityState.Modified;

                Clearance clearance = context.Clearances.Find(libraryClearance.ClearanceId);
                clearance.Cleared = false;
                context.Entry(clearance).State = EntityState.Modified;

                context.SaveChanges();

                // If successful, the clearance is taken care of. so, redirect to list of clearances
                TempData["SuccessMessage"] = "The clearance request has been denied.";
                return RedirectToAction("Clearances");
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return Redirect(base.Request.UrlReferrer.ToString());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Librarian")]
        public ActionResult ClearStudent(int id)
        {
            try
            {
                LibraryClearance libraryClearance = context.LibraryClearances.Find(id);
                libraryClearance.Cleared = true;
                libraryClearance.LibarianId = User.Identity.GetUserId();
                context.Entry(libraryClearance).State = EntityState.Modified;

                PropertyClearance propertyClearance = new PropertyClearance
                {
                    ClearanceId = libraryClearance.ClearanceId
                };
                context.PropertyClearances.Add(propertyClearance);

                context.SaveChanges();

                // If successful, the clearance is taken care of. so, redirect to list of clearances
                TempData["SuccessMessage"] = "The student has been cleared.";
                return RedirectToAction("Clearances");
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return Redirect(base.Request.UrlReferrer.ToString());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Librarian")]
        public ActionResult ReturnBook(Loan model)
        {
            try
            {
                Loan loan = context.Loans.Find(model.LoanId);
                loan.Fine = model.Fine;
                loan.ReturnDate = DateTime.Now;
                loan.LibrarianId = User.Identity.GetUserId();
                context.Entry(loan).State = EntityState.Modified;
                context.SaveChanges();

                TempData["SuccessMessage"] = "Book has been returned.";
                return RedirectToAction("Loans");

            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return Redirect(base.Request.UrlReferrer.ToString());
        }

        [Authorize(Roles="Student")]
        public ActionResult Borrowed()
        {
            string studentId = User.Identity.GetUserId();
            IEnumerable<Loan> loans = context.Loans.Where(m => m.StudentId == studentId).Where(m=>m.ReturnDate == null).Include("Book").Include("Librarian").ToList();
            return View(loans);
        }
    }
}