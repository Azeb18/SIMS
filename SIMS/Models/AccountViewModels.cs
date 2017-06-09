using SIMS.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SIMS.Models
{
    public class IdentificationViewModel
    {
        [Required]
        public string StudentIdNumber { get; set; }
        public Student Student { get; set; }
    }

    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterStudentViewModel
    {
        // Personal Information
        [Required]
        [Display(Name="First Name")]
        [StringLength(15,MinimumLength=3)]
        [Name]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Middle Name")]
        [StringLength(15, MinimumLength = 3)]
        [Name]
        public string MiddleName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [StringLength(15, MinimumLength = 3)]
        [Name]
        public string LastName { get; set; }
        [Required]
        [Sex]
        public char Sex { get; set; }
        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }
        // Address Information
        [Required]
        public string Region { get; set; }
        [Required]
        public string Town { get; set; }
        [Required]
        [Display(Name="Sub-city")]
        public string SubCity { get; set; }
        [Required]
        public int Woreda { get; set; }
        [Required]
        [Display(Name="House Number")]
        public string HouseNumber { get; set; }
        // Enrollement Information
        [Required]
        [Display(Name = "Program")]
        public int ProgramId { get; set; }
        [Required]
        [Display(Name = "Level")]
        public int LevelId { get; set; }
        // Account Details
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string PhoneNumber { get; set; }
        // Options
        public IEnumerable<Program> Programs { get; set; }
        public IEnumerable<Level> Levels { get; set; }
    }

    public class RegisterTeacherViewModel
    {
        // Personal Information
        [Required]
        [Display(Name = "First Name")]
        [StringLength(15, MinimumLength = 3)]
        [Name]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Middle Name")]
        [StringLength(15, MinimumLength = 3)]
        [Name]
        public string MiddleName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [StringLength(15, MinimumLength = 3)]
        [Name]
        public string LastName { get; set; }
        [Required]
        [Sex]
        public char Sex { get; set; }
        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }
        // Address Information
        [Required]
        public string Region { get; set; }
        [Required]
        public string Town { get; set; }
        [Required]
        [Display(Name = "Sub-city")]
        public string SubCity { get; set; }
        [Required]
        public int Woreda { get; set; }
        [Required]
        [Display(Name = "House Number")]
        public string HouseNumber { get; set; }
        // Account Details
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string PhoneNumber { get; set; }
    }

    public class RegisterLibrarianViewModel
    {
        // Personal Information
        [Required]
        [Display(Name = "First Name")]
        [StringLength(15, MinimumLength = 3)]
        [Name]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Middle Name")]
        [StringLength(15, MinimumLength = 3)]
        [Name]
        public string MiddleName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [StringLength(15, MinimumLength = 3)]
        [Name]
        public string LastName { get; set; }
        [Required]
        [Sex]
        public char Sex { get; set; }
        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }
        // Address Information
        [Required]
        public string Region { get; set; }
        [Required]
        public string Town { get; set; }
        [Required]
        [Display(Name = "Sub-city")]
        public string SubCity { get; set; }
        [Required]
        public int Woreda { get; set; }
        [Required]
        [Display(Name = "House Number")]
        public string HouseNumber { get; set; }
        // Account Details
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string PhoneNumber { get; set; }
    }

    public class RegisterRegistrarViewModel
    {
        // Personal Information
        [Required]
        [Display(Name = "First Name")]
        [StringLength(15, MinimumLength = 3)]
        [Name]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Middle Name")]
        [StringLength(15, MinimumLength = 3)]
        [Name]
        public string MiddleName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [StringLength(15, MinimumLength = 3)]
        [Name]
        public string LastName { get; set; }
        [Required]
        [Sex]
        public char Sex { get; set; }
        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }
        // Address Information
        [Required]
        public string Region { get; set; }
        [Required]
        public string Town { get; set; }
        [Required]
        [Display(Name = "Sub-city")]
        public string SubCity { get; set; }
        [Required]
        public int Woreda { get; set; }
        [Required]
        [Display(Name = "House Number")]
        public string HouseNumber { get; set; }
        // Account Details
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string PhoneNumber { get; set; }
    }

    public class RegisterPropertyViewModel
    {
        // Personal Information
        [Required]
        [Display(Name = "First Name")]
        [StringLength(15, MinimumLength = 3)]
        [Name]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Middle Name")]
        [StringLength(15, MinimumLength = 3)]
        [Name]
        public string MiddleName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [StringLength(15, MinimumLength = 3)]
        [Name]
        public string LastName { get; set; }
        [Required]
        [Sex]
        public char Sex { get; set; }
        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }
        // Address Information
        [Required]
        public string Region { get; set; }
        [Required]
        public string Town { get; set; }
        [Required]
        [Display(Name = "Sub-city")]
        public string SubCity { get; set; }
        [Required]
        public int Woreda { get; set; }
        [Required]
        [Display(Name = "House Number")]
        public string HouseNumber { get; set; }
        // Account Details
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string PhoneNumber { get; set; }
    }

    public class RegisterAcademicDeanViewModel
    {
        // Personal Information
        [Required]
        [Display(Name = "First Name")]
        [StringLength(15, MinimumLength = 3)]
        [Name]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Middle Name")]
        [StringLength(15, MinimumLength = 3)]
        [Name]
        public string MiddleName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [StringLength(15, MinimumLength = 3)]
        [Name]
        public string LastName { get; set; }
        [Required]
        [Sex]
        public char Sex { get; set; }
        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }
        // Address Information
        [Required]
        public string Region { get; set; }
        [Required]
        public string Town { get; set; }
        [Required]
        [Display(Name = "Sub-city")]
        public string SubCity { get; set; }
        [Required]
        public int Woreda { get; set; }
        [Required]
        [Display(Name = "House Number")]
        public string HouseNumber { get; set; }
        // Account Details
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string PhoneNumber { get; set; }
    }

    public class RegisterCoordinatorViewModel
    {
        // Personal Information
        [Required]
        [Display(Name = "First Name")]
        [StringLength(15, MinimumLength = 3)]
        [Name]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Middle Name")]
        [StringLength(15, MinimumLength = 3)]
        [Name]
        public string MiddleName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [StringLength(15, MinimumLength = 3)]
        [Name]
        public string LastName { get; set; }
        [Required]
        [Sex]
        public char Sex { get; set; }
        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }
        // Address Information
        [Required]
        public string Region { get; set; }
        [Required]
        public string Town { get; set; }
        [Required]
        [Display(Name = "Sub-city")]
        public string SubCity { get; set; }
        [Required]
        public int Woreda { get; set; }
        [Required]
        [Display(Name = "House Number")]
        public string HouseNumber { get; set; }
        // Account Details
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string PhoneNumber { get; set; }
        // Daytime or Extension coordinator
        [Display(Name="Students to Coordinate")]
        public string Type { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class RegisterSecurityViewModel
    {
        // Personal Information
        [Required]
        [Display(Name = "First Name")]
        [StringLength(15, MinimumLength = 3)]
        [Name]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Middle Name")]
        [StringLength(15, MinimumLength = 3)]
        [Name]
        public string MiddleName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [StringLength(15, MinimumLength = 3)]
        [Name]
        public string LastName { get; set; }
        [Required]
        [Sex]
        public char Sex { get; set; }
        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }
        // Address Information
        [Required]
        public string Region { get; set; }
        [Required]
        public string Town { get; set; }
        [Required]
        [Display(Name = "Sub-city")]
        public string SubCity { get; set; }
        [Required]
        public int Woreda { get; set; }
        [Required]
        [Display(Name = "House Number")]
        public string HouseNumber { get; set; }
        // Account Details
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string PhoneNumber { get; set; }
    }

    public class RegisterCafeViewModel
    {
        // Personal Information
        [Required]
        [Display(Name = "First Name")]
        [StringLength(15, MinimumLength = 3)]
        [Name]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Middle Name")]
        [StringLength(15, MinimumLength = 3)]
        [Name]
        public string MiddleName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [StringLength(15, MinimumLength = 3)]
        [Name]
        public string LastName { get; set; }
        [Required]
        [Sex]
        public char Sex { get; set; }
        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }
        // Address Information
        [Required]
        public string Region { get; set; }
        [Required]
        public string Town { get; set; }
        [Required]
        [Display(Name = "Sub-city")]
        public string SubCity { get; set; }
        [Required]
        public int Woreda { get; set; }
        [Required]
        [Display(Name = "House Number")]
        public string HouseNumber { get; set; }
        // Account Details
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string PhoneNumber { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
