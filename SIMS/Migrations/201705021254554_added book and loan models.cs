namespace SIMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedbookandloanmodels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Author = c.String(),
                        EditionYear = c.String(),
                        Amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookId);
            
            CreateTable(
                "dbo.Loans",
                c => new
                    {
                        LoanId = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        StudentId = c.String(maxLength: 128),
                        LibrarianId = c.String(maxLength: 128),
                        LoanDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(nullable: false),
                        Fine = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.LoanId)
                .ForeignKey("dbo.AspNetUsers", t => t.LibrarianId)
                .ForeignKey("dbo.AspNetUsers", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.LibrarianId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Loans", "StudentId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Loans", "LibrarianId", "dbo.AspNetUsers");
            DropIndex("dbo.Loans", new[] { "LibrarianId" });
            DropIndex("dbo.Loans", new[] { "StudentId" });
            DropTable("dbo.Loans");
            DropTable("dbo.Books");
        }
    }
}
