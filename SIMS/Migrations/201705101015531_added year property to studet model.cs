namespace SIMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedyearpropertytostudetmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Year", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Year");
        }
    }
}
