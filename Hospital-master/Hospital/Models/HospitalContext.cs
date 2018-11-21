using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Hospital.Models
{
    public class HospitalContext : IdentityDbContext
    {
        public HospitalContext() : base("DefaultConnection")
        {
        }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Doctor> Doctors { get; set; }
        
        
        
    }
}