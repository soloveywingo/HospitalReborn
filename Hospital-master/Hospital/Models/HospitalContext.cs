using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Hospital.Models
{
    public class HospitalContext : DbContext
    {
        public HospitalContext() : base("Hospital")
        {
        }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<RegisterUser> RegisterUsers { get; set; } 
        
        
    }
}