namespace Hospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patient1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "ImageUrl", c => c.String());
            DropColumn("dbo.Patients", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Patients", "Image", c => c.Binary());
            DropColumn("dbo.Patients", "ImageUrl");
        }
    }
}
