using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIMS.Models
{
    public class StudentFile
    {
        public int StudentFileId { get; set; }
        [Required]
        public string StudentId { get; set; }
        [Required]
        public string File { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime UploadedOn { get; set; }
       
        // Navigation Property
        public virtual Student Student { get; set; }
    }


    // View models
    public class UploadFileViewModel
    {
        [Required]
        public HttpPostedFileBase File { get; set; }
        [Required]
        public string Description { get; set; }
    }
    
    public class UploadedFilesViewModel
    {
        public int StudentFileId { get; set; }
        public IEnumerable<StudentFile> StudentFiles { get; set; }
    }
}