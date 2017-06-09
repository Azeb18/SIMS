namespace SIMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedclearedpropertytoclearancemodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clearances", "Cleared", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clearances", "Cleared");
        }
    }
}
