namespace SIMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class smallmodificationtolibraryclearance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LibraryClearances",
                c => new
                    {
                        LibraryClearanceId = c.Int(nullable: false, identity: true),
                        ClearanceId = c.Int(nullable: false),
                        LibarianId = c.String(),
                        Cleared = c.Boolean(),
                        Librarian_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.LibraryClearanceId)
                .ForeignKey("dbo.Clearances", t => t.ClearanceId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Librarian_Id)
                .Index(t => t.ClearanceId)
                .Index(t => t.Librarian_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LibraryClearances", "Librarian_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.LibraryClearances", "ClearanceId", "dbo.Clearances");
            DropIndex("dbo.LibraryClearances", new[] { "Librarian_Id" });
            DropIndex("dbo.LibraryClearances", new[] { "ClearanceId" });
            DropTable("dbo.LibraryClearances");
        }
    }
}
