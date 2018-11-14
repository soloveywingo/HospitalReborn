using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Specialization { get; set; }

        public virtual ICollection<Patient> Patients { get; set; }

        public Doctor()
        {
        }

        private void CountAllPatients()
        {
            HospitalContext db = new HospitalContext();
        }
        
    }
}