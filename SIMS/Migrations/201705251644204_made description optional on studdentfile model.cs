namespace SIMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class madedescriptionoptionalonstuddentfilemodel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StudentFiles", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StudentFiles", "Description", c => c.String(nullable: false));
        }
    }
}
