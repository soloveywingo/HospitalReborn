using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Models
{
    public class RegisterUser
    {
        public int Id { get; set; }
        
        
        public string Login { get; set; }
        public string Name { get; set; }
        public string Pass { get; set; }
        public string Pass2 { get; set; }
        public string Image { get; set; }
        public DateTime BirthDay { get; set; }

        public RegisterUser()
        {
            BirthDay = DateTime.Now;
        }
    }
}