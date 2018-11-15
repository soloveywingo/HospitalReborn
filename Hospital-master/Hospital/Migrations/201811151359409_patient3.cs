namespace Hospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patient3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Patients", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Patients", "Name", c => c.String(nullable: false, maxLength: 30));
        }
    }
}
