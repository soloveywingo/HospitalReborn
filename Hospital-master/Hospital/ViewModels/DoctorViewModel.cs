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
    public class DoctorViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }


        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        public string Specialization { get; set; }

        public virtual ICollection<Patient> Patients { get; set; }

        public HttpPostedFileBase DoctorImage { get; set; }

        public static Doctor ToDoctor(DoctorViewModel doctorViewModel)
        {
            
            Doctor doctor = new Doctor
            {
                Name = doctorViewModel.Name,
                Specialization = doctorViewModel.Specialization,
                Email = doctorViewModel.Email

            };
            return doctor;
        }
        

    }
}