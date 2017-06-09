namespace SIMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeduploadedonpropertytostudentfilemodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentFiles", "UploadedOn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentFiles", "UploadedOn");
        }
    }
}
