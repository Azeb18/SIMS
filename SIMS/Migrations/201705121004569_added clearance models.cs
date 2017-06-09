namespace SIMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedclearancemodels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AcademicDeanClearances",
                c => new
                    {
                        AcademicDeanClearanceId = c.Int(nullable: false, identity: true),
                        ClearanceId = c.Int(nullable: false),
                        AcademicDeanId = c.String(maxLength: 128),
                        Cleared = c.Boolean(),
                    })
                .PrimaryKey(t => t.AcademicDeanClearanceId)
                .ForeignKey("dbo.AspNetUsers", t => t.AcademicDeanId)
                .ForeignKey("dbo.Clearances", t => t.ClearanceId, cascadeDelete: true)
                .Index(t => t.ClearanceId)
                .Index(t => t.AcademicDeanId);
            
            CreateTable(
                "dbo.PropertyClearances",
                c => new
                    {
                        PropertyClearanceId = c.Int(nullable: false, identity: true),
                        ClearanceId = c.Int(nullable: false),
                        PropertyId = c.String(maxLength: 128),
                        Cleared = c.Boolean(),
                    })
                .PrimaryKey(t => t.PropertyClearanceId)
                .ForeignKey("dbo.Clearances", t => t.ClearanceId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.PropertyId)
                .Index(t => t.ClearanceId)
                .Index(t => t.PropertyId);
            
            CreateTable(
                "dbo.RegistrarClearances",
                c => new
                    {
                        RegistrarClearanceId = c.Int(nullable: false, identity: true),
                        ClearanceId = c.Int(nullable: false),
                        RegistrarId = c.String(maxLength: 128),
                        Cleared = c.Boolean(),
                    })
                .PrimaryKey(t => t.RegistrarClearanceId)
                .ForeignKey("dbo.Clearances", t => t.ClearanceId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.RegistrarId)
                .Index(t => t.ClearanceId)
                .Index(t => t.RegistrarId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RegistrarClearances", "RegistrarId", "dbo.AspNetUsers");
            DropForeignKey("dbo.RegistrarClearances", "ClearanceId", "dbo.Clearances");
            DropForeignKey("dbo.PropertyClearances", "PropertyId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PropertyClearances", "ClearanceId", "dbo.Clearances");
            DropForeignKey("dbo.AcademicDeanClearances", "ClearanceId", "dbo.Clearances");
            DropForeignKey("dbo.AcademicDeanClearances", "AcademicDeanId", "dbo.AspNetUsers");
            DropIndex("dbo.RegistrarClearances", new[] { "RegistrarId" });
            DropIndex("dbo.RegistrarClearances", new[] { "ClearanceId" });
            DropIndex("dbo.PropertyClearances", new[] { "PropertyId" });
            DropIndex("dbo.PropertyClearances", new[] { "ClearanceId" });
            DropIndex("dbo.AcademicDeanClearances", new[] { "AcademicDeanId" });
            DropIndex("dbo.AcademicDeanClearances", new[] { "ClearanceId" });
            DropTable("dbo.RegistrarClearances");
            DropTable("dbo.PropertyClearances");
            DropTable("dbo.AcademicDeanClearances");
        }
    }
}
