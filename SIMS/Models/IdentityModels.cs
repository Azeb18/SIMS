using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using SIMS.Validators;
using System.ComponentModel.DataAnnotations;

namespace SIMS.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string IdNumber { get; set; }
        // Personal Information
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public char Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        // Address Information
        public string Region { get; set; }
        public string Town { get; set; }
        public string SubCity { get; set; }
        public int Woreda { get; set; }
        public string HouseNumber { get; set; }
        // Profile Picture
        public string ProfilePicture { get; set; }
    }

    [MetadataType(typeof(ClassMetadata))]
    public class Student : ApplicationUser
    {
        // Enrollement Information
        public int ProgramId { get; set; }
        public int LevelId { get; set; }
        public DateTime RegisterationDate { get; set; }
        public int Year { get; set; }
        //Section Placement
        public string Section { get; set; }

        // Navigation Properties
        public virtual Program Program { get; set; }
        public virtual Level Level { get; set; }

        class ClassMetadata
        {
            [Display(Name="Student Id")]
            public string IdNumber { get; set; }
        }
    }

    [MetadataType(typeof(ClassMetadata))]
    public class Teacher : ApplicationUser
    {
        class ClassMetadata
        {
            [Display(Name = "Teacher Id")]
            public string IdNumber { get; set; }
        }
    }

    [MetadataType(typeof(ClassMetadata))]
    public class Registrar : ApplicationUser
    {
    
        class ClassMetadata
        {
            [Display(Name="Registrar Id")]
            public string IdNumber { get; set; }
        }
    }

    [MetadataType(typeof(ClassMetadata))]
    public class Librarian : ApplicationUser
    {

        class ClassMetadata
        {
            [Display(Name = "Librarian Id")]
            public string IdNumber { get; set; }
        }
    }

    [MetadataType(typeof(ClassMetadata))]
    public class Property : ApplicationUser
    {

        class ClassMetadata
        {
            [Display(Name = "Property Staff Id")]
            public string IdNumber { get; set; }
        }
    }

    [MetadataType(typeof(ClassMetadata))]
    public class Coordinator : ApplicationUser
    {

        class ClassMetadata
        {
            [Display(Name = "Coordinator Id")]
            public string IdNumber { get; set; }
        }
    }

    [MetadataType(typeof(ClassMetadata))]
    public class AcademicDean : ApplicationUser
    {

        class ClassMetadata
        {
            [Display(Name = "Academic Dean Id")]
            public string IdNumber { get; set; }
        }
    }

    [MetadataType(typeof(ClassMetadata))]
    public class Security : ApplicationUser
    {

        class ClassMetadata
        {
            [Display(Name = "Security")]
            public string IdNumber { get; set; }
        }
    }

    [MetadataType(typeof(ClassMetadata))]
    public class Cafe : ApplicationUser
    {

        class ClassMetadata
        {
            [Display(Name = "Cafe")]
            public string IdNumber { get; set; }
        }
    }


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        // Accounts
        public DbSet<Student> Students { get; set; }
        public DbSet<Registrar> Registrars { get; set; }
        public DbSet<Librarian> Librarians { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Property> Property { get; set; }
        public DbSet<AcademicDean> AcademicDean { get; set; }
        public DbSet<Coordinator> Coordinators { get; set; }
        public DbSet<Security> Security { get; set; }
        public DbSet<Cafe> Cafe { get; set; }

        // Programs and Levels
        public DbSet<Program> Programs { get; set; }
        public DbSet<Level> Levels { get; set; }
        // Library Models
        public DbSet<Book> Books { get; set; }
        public DbSet<Loan> Loans { get; set; }
        // Clearance Models
        public DbSet<Clearance> Clearances { get; set; }
        public DbSet<ClearanceReason> ClearanceReasons { get; set; }
        public DbSet<LibraryClearance> LibraryClearances { get; set; }
        public DbSet<PropertyClearance> PropertyClearances { get; set; }
        public DbSet<AcademicDeanClearance> AcademicDeanClearances { get; set; }
        public DbSet<RegistrarClearance> RegistrarClearances { get; set; }
        // Courses
        public DbSet<Course> Curriculum { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<TeacherCourse> TeacherCourses { get; set; }
        // Grade
        public DbSet<Grade> Grades { get; set; }
        // File
        public DbSet<StudentFile> StudentFiles { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}