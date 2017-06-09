namespace SIMS.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using SIMS.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SIMS.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SIMS.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //context.Roles.AddOrUpdate(
            //    new IdentityRole { Name = "Security" },
            //    new IdentityRole { Name = "Cafe" }
            //);

            //context.Programs.AddOrUpdate(
            //    new Program { Name = "Regular", IdPrefix = "R"},
            //    new Program { Name = "Extension", IdPrefix = "Ex"},
            //    new Program { Name = "Distance", IdPrefix = "DD"}
            //);

            //context.Levels.AddOrUpdate(
                //new Level { Name = "Bachelors Degree" },
                //new Level { Name = "Masters Degree" },
                //new Level { Name = "Diploma"}
            //);

            //context.Books.AddOrUpdate(
            //    new Book { Title = "Fundamentals of Programming" , Author = "Lee Adams" , EditionYear = "1990" , Amount = 4 },
            //    new Book { Title = "Object Oriented Programming" , Author = "Abebe Ayele" , EditionYear = "2010" , Amount = 2 },
            //    new Book { Title = "Head First Python" , Author = "James Poole" , EditionYear = "2002" , Amount = 1 }
            //);

            //context.ClearanceReasons.AddOrUpdate(
            //    new ClearanceReason { Reason = "Registeration" },
            //    new ClearanceReason { Reason = "Withdrawal" },
            //    new ClearanceReason { Reason = "Id Renewal" }
            //);

            //context.Curriculum.AddOrUpdate(
                //// First Year
                //new Course { CourseNumber = "Lang. 101" , Title = "College English I", Year = 1, Semester = 1, CreditHours = 3 , LevelId = 1, ProgramId = 1 },
                //new Course { CourseNumber = "Lang. 111" , Title = "Geez I: Spoken Geez: Reading Skills and Grammer", Year = 1, Semester = 1, CreditHours = 3 , LevelId = 1, ProgramId = 1 },
                //new Course { CourseNumber = "Lang. 121" , Title = "Hebrew I", Year = 1, Semester = 1, CreditHours = 2 , LevelId = 1, ProgramId = 1 },
                //new Course { CourseNumber = "Lang. 131" , Title = "Greek I", Year = 1, Semester = 1, CreditHours = 2 , LevelId = 1, ProgramId = 1 },
                //new Course { CourseNumber = "Bible. 101" , Title = "Introduction to Bible Studies", Year = 1, Semester = 1, CreditHours = 2 , LevelId = 1, ProgramId = 1 },
                //new Course { CourseNumber = "Hist. 101" , Title = "History of Religion I", Year = 1, Semester = 1, CreditHours = 2 , LevelId = 1, ProgramId = 1 },
                //new Course { CourseNumber = "Qu.Mt. 101" , Title = "Quantitative Methods", Year = 1, Semester = 1, CreditHours = 3 , LevelId = 1, ProgramId = 1 },
                //new Course { CourseNumber = "Lang. 102" , Title = "College English II", Year = 1, Semester = 2, CreditHours = 3 , LevelId = 1, ProgramId = 1 },
                //new Course { CourseNumber = "Lang. 112" , Title = "Geez II: Geek Composition & Literature", Year = 1, Semester = 2, CreditHours = 3 , LevelId = 1, ProgramId = 1 },
                //new Course { CourseNumber = "Lang. 122" , Title = "Hebrew II", Year = 1, Semester = 2, CreditHours = 2 , LevelId = 1, ProgramId = 1 },
                //new Course { CourseNumber = "Lang. 132" , Title = "Greek II", Year = 1, Semester = 2, CreditHours = 3 , LevelId = 1, ProgramId = 1 },
                //new Course { CourseNumber = "Bibl. 102" , Title = "Old Testaments I", Year = 1, Semester = 2, CreditHours = 3 , LevelId = 1, ProgramId = 1 },
                //new Course { CourseNumber = "Bibl. 112" , Title = "New Testaments I", Year = 1, Semester = 2, CreditHours = 3 , LevelId = 1, ProgramId = 1 },
                //new Course { CourseNumber = "Hist. 102" , Title = "History of Religion II", Year = 1, Semester = 2, CreditHours = 2 , LevelId = 1, ProgramId = 1 },

                //// Third Year
                //new Course { CourseNumber = "Bibl. 301" , Title = "Old Testaments IV", Year = 3, Semester = 1, CreditHours = 3 , LevelId = 1, ProgramId = 1 },
                //new Course { CourseNumber = "Bibl. 311" , Title = "New Testaments IV", Year = 3, Semester = 1, CreditHours = 3 , LevelId = 1, ProgramId = 1 },
                //new Course { CourseNumber = "Hist. 301" , Title = "Church History II", Year = 3, Semester = 1, CreditHours = 3 , LevelId = 1, ProgramId = 1 },
                //new Course { CourseNumber = "Theo. 301" , Title = "Dogma III", Year = 3, Semester = 1, CreditHours = 3 , LevelId = 1, ProgramId = 1 },
                //new Course { CourseNumber = "Theo. 311" , Title = "Patrology II", Year = 3, Semester = 1, CreditHours = 3 , LevelId = 1, ProgramId = 1 },
                //new Course { CourseNumber = "Psyc. 301" , Title = "General Psychology I", Year = 3, Semester = 1, CreditHours = 3 , LevelId = 1, ProgramId = 1 },
                //new Course { CourseNumber = "Bibl. 302" , Title = "Old Testaments V", Year = 3, Semester = 2, CreditHours = 3 , LevelId = 1, ProgramId = 1 },
                //new Course { CourseNumber = "Bibl. 312" , Title = "New Testaments V", Year = 3, Semester = 2, CreditHours = 3 , LevelId = 1, ProgramId = 1 },
                //new Course { CourseNumber = "Hist. 302" , Title = "Church History III", Year = 3, Semester = 2, CreditHours = 2 , LevelId = 1, ProgramId = 1 },
                //new Course { CourseNumber = "Hist. 312" , Title = "History of Ethiopian Orthodox Church I", Year = 3, Semester = 2, CreditHours = 3 , LevelId = 1, ProgramId = 1 },
                //new Course { CourseNumber = "Theo. 302" , Title = "Church Ethics I", Year = 3, Semester = 2, CreditHours = 2 , LevelId = 1, ProgramId = 1 },
                //new Course { CourseNumber = "Theo. 312" , Title = "Liturgy I", Year = 3, Semester = 2, CreditHours = 3 , LevelId = 1, ProgramId = 1 },
                //new Course { CourseNumber = "Psyc. 302" , Title = "General Psychology II", Year = 3, Semester = 2, CreditHours = 3 , LevelId = 1, ProgramId = 1 }

            //);            

            //context.Grades.AddOrUpdate(
            //    new Grade { Name = "A", Value = 4 },
            //    new Grade { Name = "B", Value = 3 },
            //    new Grade { Name = "C", Value = 2 },
            //    new Grade { Name = "D", Value = 1 },
            //    new Grade { Name = "F", Value = 0 }
            //);

            
                            
        }
    }
}
