namespace SIMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class madegradeidnullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StudentCourses", "GradeId", "dbo.Grades");
            DropIndex("dbo.StudentCourses", new[] { "GradeId" });
            AlterColumn("dbo.StudentCourses", "GradeId", c => c.Int());
            CreateIndex("dbo.StudentCourses", "GradeId");
            AddForeignKey("dbo.StudentCourses", "GradeId", "dbo.Grades", "GradeId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentCourses", "GradeId", "dbo.Grades");
            DropIndex("dbo.StudentCourses", new[] { "GradeId" });
            AlterColumn("dbo.StudentCourses", "GradeId", c => c.Int(nullable: false));
            CreateIndex("dbo.StudentCourses", "GradeId");
            AddForeignKey("dbo.StudentCourses", "GradeId", "dbo.Grades", "GradeId", cascadeDelete: true);
        }
    }
}
