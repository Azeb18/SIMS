using SIMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace SIMS.Controllers
{
    public class FileController : Controller
    {
        private ApplicationDbContext context;

        public FileController()
        {
            context = new ApplicationDbContext();
        }

        // GET: File/Upload
        public ActionResult Upload()
        {
            UploadFileViewModel model = new UploadFileViewModel();
            return View(model);
        }

        // POST: File/Upload
        [Authorize(Roles = "Student")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(UploadFileViewModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    if(model.File.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(model.File.FileName);
                        string path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);
                        model.File.SaveAs(path);

                        StudentFile studentFile = new StudentFile
                        {
                            StudentId = User.Identity.GetUserId(),
                            File = fileName,
                            Description = model.Description,
                            UploadedOn = DateTime.Now
                        };

                        context.StudentFiles.Add(studentFile);
                        context.SaveChanges();

                        TempData["SuccessMessage"] = "File was successfully uploaded!";
                    }
                    else
                    {
                        throw new Exception("Please select a valid file.");
                    }
                }
                catch (Exception)
                {
                    TempData["ErrorMessage"] = "File was not uploaded, please try again.";
                }
            }            
            return RedirectToAction("Upload");
        }

        // GET: File/Upload
        [Authorize(Roles = "Student")]
        public ActionResult UploadedFiles()
        {
            UploadedFilesViewModel model = new UploadedFilesViewModel();
            string studentId = User.Identity.GetUserId();
            IEnumerable<StudentFile> studentFiles = context.StudentFiles.Where(m => m.StudentId == studentId).ToList();

            model.StudentFiles = studentFiles;
            return View(model);
        }


        public ActionResult Delete(int StudentFileId)
        {
            try
            {
                StudentFile studentFile = context.StudentFiles.Find(StudentFileId);
                string filePath = Request.MapPath("~/UploadedFiles/") + studentFile.File;

                if(System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                context.StudentFiles.Remove(studentFile);
                context.SaveChanges();

                TempData["SuccessMessage"] = "File has been deleted";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Could not delete file, please try again.";
            }
            return RedirectToAction("UploadedFiles");
        }
    }
}