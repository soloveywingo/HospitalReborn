using Hospital.Models;
using Hospital.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Controllers
{
    public class PatientsController : Controller
    {
        private HospitalContext db = new HospitalContext();
        //GET: Patients

        public ActionResult Index(string searching)
        {
            return View(db.Patients.Where(patient => patient.Name.Contains(searching)
            || searching == null).ToList());
        }
        // GET: Patients/Details/5
        public ActionResult Details(int? id)
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

        // GET: Patients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PatientViewModel patientViewModel)
        {
            if (ModelState.IsValid)
            {
                Patient patient = PatientViewModel.ToPatient(patientViewModel);
                patient.Doctors.AddRange(db.Doctors.ToList().Where(doctor => patientViewModel.DoctorsIds.Contains(doctor.Id)));


                patient.ImageUrl = GetImageStringPath(patientViewModel);
                patientViewModel.UserImage.SaveAs(Path.Combine(Server.MapPath("~/AppFile/PatientPictures"), 
                    patient.ImageUrl));
                

                db.Patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patientViewModel);
        }

        

        // GET: Patients/Edit/5
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

        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Patients/Delete/5
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

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = db.Patients.Find(id);
            db.Patients.Remove(patient);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
    }
}