namespace SIMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedteachercoursemodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TeacherCourses",
                c => new
                    {
                        TeacherCourseId = c.Int(nullable: false, identity: true),
                        TeacherId = c.String(nullable: false, maxLength: 128),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TeacherCourseId)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.TeacherId, cascadeDelete: false)
                .Index(t => t.TeacherId)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeacherCourses", "TeacherId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TeacherCourses", "CourseId", "dbo.Courses");
            DropIndex("dbo.TeacherCourses", new[] { "CourseId" });
            DropIndex("dbo.TeacherCourses", new[] { "TeacherId" });
            DropTable("dbo.TeacherCourses");
        }
    }
}
