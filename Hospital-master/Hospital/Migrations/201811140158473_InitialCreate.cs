namespace Hospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Specialization = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Status = c.Int(nullable: false),
                        DayOfBirth = c.DateTime(nullable: false),
                        TaxCode = c.String(),
                        AttendingDoctorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RegisterUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Name = c.String(),
                        Pass = c.String(),
                        Pass2 = c.String(),
                        Image = c.String(),
                        BirthDay = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PatientDoctors",
                c => new
                    {
                        Patient_Id = c.Int(nullable: false),
                        Doctor_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Patient_Id, t.Doctor_Id })
                .ForeignKey("dbo.Patients", t => t.Patient_Id, cascadeDelete: true)
                .ForeignKey("dbo.Doctors", t => t.Doctor_Id, cascadeDelete: true)
                .Index(t => t.Patient_Id)
                .Index(t => t.Doctor_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PatientDoctors", "Doctor_Id", "dbo.Doctors");
            DropForeignKey("dbo.PatientDoctors", "Patient_Id", "dbo.Patients");
            DropIndex("dbo.PatientDoctors", new[] { "Doctor_Id" });
            DropIndex("dbo.PatientDoctors", new[] { "Patient_Id" });
            DropTable("dbo.PatientDoctors");
            DropTable("dbo.RegisterUsers");
            DropTable("dbo.Patients");
            DropTable("dbo.Doctors");
        }
    }
}
