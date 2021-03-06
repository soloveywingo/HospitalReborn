﻿using Hospital.Enums;
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
        
        public string Name { get; set; }

        public string Email { get; set; }
        
        public Status Status  { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DayOfBirth { get; set; }
        
        public string TaxCode { get; set; }
        
        public int AttendingDoctorId { get; set; }

        public virtual List<Doctor> Doctors { get; set; } = new List<Doctor>();

        public string ImageUrl { get; set; }
    }
}