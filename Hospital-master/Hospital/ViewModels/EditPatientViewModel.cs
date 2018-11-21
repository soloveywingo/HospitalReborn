using Hospital.Enums;
using Hospital.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospital.ViewModels
{
    public class EditPatientViewModel
    {

        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "Isn`t it to much 4 ya?")]
        public string Name{ get; set; }

        public Status Status { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DayOfBirth { get; set; }

        public string TaxCode { get; set; }

        public int AttendingDoctorId { get; set; }
        
        public List<int> DoctorsIds { get; set; }

        public List<Doctor> Doctors;

        public HttpPostedFileBase ChangedImage { get; set; }
        

        public static EditPatientViewModel ToViewModel(Patient patient)
        {
            EditPatientViewModel viewModel = new EditPatientViewModel
            {
                Name = patient.Name,
                Status = patient.Status,
                TaxCode = patient.TaxCode,
                DayOfBirth = patient.DayOfBirth,
                AttendingDoctorId = patient.AttendingDoctorId,
                Doctors = patient.Doctors
            };
            return viewModel;
        }
    }
}