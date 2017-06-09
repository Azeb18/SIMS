namespace SIMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedclearanceandclearancereason : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClearanceReasons",
                c => new
                    {
                        ClearanceReasonId = c.Int(nullable: false, identity: true),
                        Reason = c.String(),
                    })
                .PrimaryKey(t => t.ClearanceReasonId);
            
            CreateTable(
                "dbo.Clearances",
                c => new
                    {
                        ClearanceId = c.Int(nullable: false, identity: true),
                        StudentId = c.String(maxLength: 128),
                        ClearanceReasonId = c.Int(nullable: false),
                        Remark = c.String(),
                        RequestDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ClearanceId)
                .ForeignKey("dbo.ClearanceReasons", t => t.ClearanceReasonId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.ClearanceReasonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clearances", "StudentId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Clearances", "ClearanceReasonId", "dbo.ClearanceReasons");
            DropIndex("dbo.Clearances", new[] { "ClearanceReasonId" });
            DropIndex("dbo.Clearances", new[] { "StudentId" });
            DropTable("dbo.Clearances");
            DropTable("dbo.ClearanceReasons");
        }
    }
}
