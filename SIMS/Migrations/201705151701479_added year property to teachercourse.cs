namespace SIMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedyearpropertytoteachercourse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TeacherCourses", "Year", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TeacherCourses", "Year");
        }
    }
}
