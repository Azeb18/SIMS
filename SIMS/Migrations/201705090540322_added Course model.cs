namespace SIMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedCoursemodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        CourseNumber = c.String(maxLength: 20),
                        Title = c.String(),
                        Year = c.String(),
                        Semester = c.Int(nullable: false),
                        CreditHours = c.Int(nullable: false),
                        LevelId = c.Int(nullable: false),
                        ProgramId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseId)
                .ForeignKey("dbo.Levels", t => t.LevelId, cascadeDelete: true)
                .ForeignKey("dbo.Programs", t => t.ProgramId, cascadeDelete: true)
                .Index(t => t.CourseNumber, unique: true)
                .Index(t => t.LevelId)
                .Index(t => t.ProgramId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "ProgramId", "dbo.Programs");
            DropForeignKey("dbo.Courses", "LevelId", "dbo.Levels");
            DropIndex("dbo.Courses", new[] { "ProgramId" });
            DropIndex("dbo.Courses", new[] { "LevelId" });
            DropIndex("dbo.Courses", new[] { "CourseNumber" });
            DropTable("dbo.Courses");
        }
    }
}
