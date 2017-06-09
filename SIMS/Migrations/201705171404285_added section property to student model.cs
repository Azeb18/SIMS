namespace SIMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedsectionpropertytostudentmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Section", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Section");
        }
    }
}
