using Hospital.Enums;
using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital.ViewModels
{
    public class PatientViewModel
    {
        private static HospitalContext db = new HospitalContext();

        public int Id { get; set; }

        public string Name { get; set; }

        public Status Status { get; set; }

        public DateTime DayOfBirth { get; set; }

        public string TaxCode { get; set; }

        public int AttendingDoctorId { get; set; }
        
        public List<int> DoctorsIds { get; set; }

        public static IEnumerable<Doctor> Doctors = db.Doctors;
        

        public static Patient ToPatient(PatientViewModel patientViewModel)
        {
            Patient patient = new Patient
            {
                Name = patientViewModel.Name,
                Status = patientViewModel.Status,
                TaxCode = patientViewModel.TaxCode,
                DayOfBirth = patientViewModel.DayOfBirth,
                AttendingDoctorId = patientViewModel.AttendingDoctorId,

            };
            return patient;
        }
        

    }
}