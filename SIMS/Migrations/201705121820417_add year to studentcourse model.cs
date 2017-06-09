namespace SIMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addyeartostudentcoursemodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentCourses", "Year", c => c.String(nullable: false));
            AddColumn("dbo.StudentCourses", "Semester", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentCourses", "Semester");
            DropColumn("dbo.StudentCourses", "Year");
        }
    }
}
