namespace SIMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class madereturndateandfinenullableintheloanmodel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Loans", "ReturnDate", c => c.DateTime());
            AlterColumn("dbo.Loans", "Fine", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Loans", "Fine", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Loans", "ReturnDate", c => c.DateTime(nullable: false));
        }
    }
}
