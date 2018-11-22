namespace Hospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class doctor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doctors", "ImageUrl");
        }
    }
}
