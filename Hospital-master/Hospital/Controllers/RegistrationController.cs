using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Controllers
{
    public class RegistrationController : Controller
    {
        private HospitalContext db = new HospitalContext();
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index([Bind(Include = "Login,Name,Pass,Pass2,Image")] RegisterUser registerUser)
        {
            if (ModelState.IsValid)
            {
                db.RegisterUsers.Add(registerUser);
                Patient newPatient = new Patient() { AttendingDoctorId = 1, Name = registerUser.Name, DayOfBirth = registerUser.BirthDay, Status = Enums.Status.Sick, TaxCode = "not confirmed" };
                db.Patients.Add(newPatient);
                db.SaveChanges();
                Response.Redirect("http://localhost:64270/Patients/Details/" + newPatient.Id);
            }
            return View();
            
        }

        
    }
}
