namespace SIMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedyeartointincoursemodel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Courses", "Year", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Courses", "Year", c => c.String());
        }
    }
}
