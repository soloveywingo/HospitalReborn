using Hospital.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospital.Models
{
    public class Patient
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter ur name!")]
        [MaxLength(30, ErrorMessage = "Isn`t it to much 4 ya?")]
        public string Name { get; set; }
        
        public Status Status  { get; set; }

        public DateTime DayOfBirth { get; set; }
        
        public string TaxCode { get; set; }
        
        public int AttendingDoctorId { get; set; }

        public List<Doctor> Doctors { get; set; } = new List<Doctor>();

        public string ImageUrl { get; set; }
    }
}