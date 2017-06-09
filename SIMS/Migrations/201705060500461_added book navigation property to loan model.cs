namespace SIMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedbooknavigationpropertytoloanmodel : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Loans", "BookId");
            AddForeignKey("dbo.Loans", "BookId", "dbo.Books", "BookId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Loans", "BookId", "dbo.Books");
            DropIndex("dbo.Loans", new[] { "BookId" });
        }
    }
}
