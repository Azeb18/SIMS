namespace SIMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedstudentfilemodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentFiles",
                c => new
                    {
                        StudentFileId = c.Int(nullable: false, identity: true),
                        StudentId = c.String(nullable: false, maxLength: 128),
                        File = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.StudentFileId)
                .ForeignKey("dbo.AspNetUsers", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentFiles", "StudentId", "dbo.AspNetUsers");
            DropIndex("dbo.StudentFiles", new[] { "StudentId" });
            DropTable("dbo.StudentFiles");
        }
    }
}
