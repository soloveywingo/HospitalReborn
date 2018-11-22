using Hospital.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Hospital.Startup))]
namespace Hospital
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsers();
        }
        private void CreateRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole
                {
                    Name = "Admin"
                };
                roleManager.Create(role);
                
                //creating super admin instantly
                var user = new ApplicationUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com"
                };
                string userPassword = "asdasd";
                var admin = UserManager.Create(user, userPassword);

                if (admin.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            }

            if (!roleManager.RoleExists("Doctor"))
            {
                var role = new IdentityRole
                {
                    Name = "Doctor"
                };
                roleManager.Create(role);

                var user = new ApplicationUser
                {
                    UserName = "doctor@gmail.com",
                    Email = "doctor@gmail.com"
                };
                string docPath = "asdasd";
                var doc = UserManager.Create(user, docPath);
                if (doc.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Doctor");
                }
                Doctor doctor = new Doctor
                {
                    Name = "Bob",
                    Specialization = "AloneDoctor",
                    Email = user.Email,
                    ImageUrl = "DoctorBob.jpg"
                };
                context.Doctors.Add(doctor);
                context.SaveChanges();


            }

            if (!roleManager.RoleExists("Patient"))
            {
                var role = new IdentityRole
                {
                    Name = "Patient"
                };
                roleManager.Create(role);

            }
        }
    }
}
