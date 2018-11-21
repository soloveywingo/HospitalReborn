using Hospital.Models;
using Hospital.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Controllers
{
   
    public class PatientsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        [Authorize(Roles = "Admin,Doctor")]
        public ActionResult Index(string searching)
        {
            return View(db.Patients.Where(patient => patient.Name.Contains(searching)
            || searching == null).ToList());
        }

        [Authorize(Roles = "Admin,Doctor,Patient")]
        public ActionResult Details(int? id)
        {
            var currentUser = UserManager.FindById(User.Identity.GetUserId());
            if (User.IsInRole("Patient"))
            {
                Patient currentPatient = db.Patients.First(p => p.Email == currentUser.Email);
                if (currentPatient.Id == id)
                {
                    Patient patient = db.Patients.Find(id);
                    return ValidateNulls(id, patient);
                }
                return RedirectToAction("Details", "Patients", new { id = currentPatient.Id });
            }
            else
            {
                Patient patient = db.Patients.Find(id);
                return ValidateNulls(id, patient);
            }

        }
        
        [Authorize(Roles = "Admin,Doctor")]
        public ActionResult Create()
        {
            PatientViewModel patientViewModel = new PatientViewModel() { Doctors = db.Doctors.ToList() };
            return View(patientViewModel);
        }

        [Authorize(Roles = "Admin,Doctor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PatientViewModel patientViewModel)
        {
            if (ModelState.IsValid)
            {
                Patient patient = PatientViewModel.ToPatient(patientViewModel);
                patient.Doctors.AddRange(db.Doctors.ToList().Where
                    (doctor => patientViewModel.DoctorsIds.Contains(doctor.Id)));


                var user = new ApplicationUser { UserName = patientViewModel.Email, Email = patientViewModel.Email };
                var result = await UserManager.CreateAsync(user, patientViewModel.Password);
                await UserManager.AddToRoleAsync(user.Id, "Patient");


                patient.ImageUrl = GetImageStringPath(patientViewModel);
                patientViewModel.UserImage.SaveAs(Path.Combine(Server.MapPath("~/AppFile/PatientPictures"), 
                    patient.ImageUrl));
                

                db.Patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patientViewModel);
        }

        [Authorize(Roles = "Admin,Doctor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        [Authorize(Roles = "Admin,Doctor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,TaxCode,DayOfBirth,Status,Doctors")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
        }

        [Authorize(Roles = "Admin,Doctor")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        [Authorize(Roles = "Admin,Doctor")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = db.Patients.Find(id);
            db.Patients.Remove(patient);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin,Doctor")]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        
        private string GetImageStringPath(PatientViewModel patientViewModel)
        {
            StringBuilder stringBuilder = new StringBuilder
                (Path.GetFileNameWithoutExtension(patientViewModel.UserImage.FileName));
            stringBuilder.Append(DateTime.Now.ToString("yymmssff") +
                Path.GetExtension(patientViewModel.UserImage.FileName));
            return stringBuilder.ToString();
        }

        private ActionResult ValidateNulls(int? id, Patient patient)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }
    }
}