using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Hospital.Infrastructure;
using Hospital.Models;
using Hospital.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Hospital.Controllers
{

    [Authorize(Roles = "Admin,Doctor")]
    public class DoctorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }
        
        public ActionResult Index(string searching)
        {
            return View(db.Doctors.Where(doctor => doctor.Name.Contains(searching)
            || searching == null).ToList());
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }
        
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DoctorViewModel doctorViewModel)
        {
            if (ModelState.IsValid)
            {

                var user = UserManager.FindByEmail(doctorViewModel.Email);
                if (user == null)
                {
                    user = new ApplicationUser { UserName = doctorViewModel.Email, Email = doctorViewModel.Email };
                    var result = await UserManager.CreateAsync(user, doctorViewModel.Password);
                    Doctor doctor = DoctorViewModel.ToDoctor(doctorViewModel);
                    await UserManager.AddToRoleAsync(user.Id, "Doctor");

                    ImageWorker imagePathGetter = new ImageWorker();
                    doctor.ImageUrl = imagePathGetter.GetImageStringPath(doctorViewModel.DoctorImage);

                    if (doctorViewModel.DoctorImage != null)
                    {
                        doctorViewModel.DoctorImage.SaveAs(Path.Combine(
                            Server.MapPath("~/AppFile/DoctorPictures"), doctor.ImageUrl));
                    }
                    db.Doctors.Add(doctor);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(doctorViewModel);
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Specialization")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doctor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doctor);
        }
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Doctor doctor = db.Doctors.Find(id);
            db.Doctors.Remove(doctor);
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
    }
}
