using Hospital.Enums;
using Hospital.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Hospital.ViewModels
{
    public class PatientViewModel
    {

        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter ur name!")]
        [MaxLength(50, ErrorMessage = "Isn`t it to much 4 ya?")]
        public string Name { get; set; }

        public Status Status { get; set; }

        public DateTime DayOfBirth { get; set; }

        public string TaxCode { get; set; }

        public int AttendingDoctorId { get; set; }

        [Required(ErrorMessage = "Choose at least one")]
        public List<int> DoctorsIds { get; set; }

        public List<Doctor> Doctors { get; set; }
        
        public HttpPostedFileBase UserImage { get; set; }

      
        public static Patient ToPatient(PatientViewModel patientViewModel)
        {

            Patient patient = new Patient
            {
                Name = patientViewModel.Name,
                Status = patientViewModel.Status,
                TaxCode = patientViewModel.TaxCode,
                DayOfBirth = patientViewModel.DayOfBirth,
                AttendingDoctorId = patientViewModel.AttendingDoctorId,
                Email = patientViewModel.Email

            };
            return patient;
        }
       
    }
}