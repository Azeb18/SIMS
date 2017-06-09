namespace SIMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedgrademodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Grades",
                c => new
                    {
                        GradeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.GradeId);
            
            AddColumn("dbo.StudentCourses", "GradeId", c => c.Int(nullable: false));
            CreateIndex("dbo.StudentCourses", "GradeId");
            AddForeignKey("dbo.StudentCourses", "GradeId", "dbo.Grades", "GradeId", cascadeDelete: true);
            DropColumn("dbo.StudentCourses", "Grade");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StudentCourses", "Grade", c => c.String());
            DropForeignKey("dbo.StudentCourses", "GradeId", "dbo.Grades");
            DropIndex("dbo.StudentCourses", new[] { "GradeId" });
            DropColumn("dbo.StudentCourses", "GradeId");
            DropTable("dbo.Grades");
        }
    }
}
