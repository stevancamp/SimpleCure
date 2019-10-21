using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using SimpleCure.Models;

[assembly: OwinStartupAttribute(typeof(SimpleCure.Startup))]
namespace SimpleCure
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (!roleManager.RoleExists("WebAdmin"))
            {
                var role = new IdentityRole();
                role.Name = "WebAdmin";
                roleManager.Create(role);
                var user = new ApplicationUser();
                user.UserName = "Stevan";
                user.Email = "Stevan.Camp12@gmail.com";
                string userPWD = "OKCgov1!";
                var chkUser = UserManager.Create(user, userPWD);
                if (chkUser.Succeeded)
                {
                    var resultl = UserManager.AddToRole(user.Id, "WebAdmin");
                }

              
            }

            if (!roleManager.RoleExists("Owner"))
            {
                var role = new IdentityRole();
                role.Name = "Owner";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Employee"))
            {
                var role = new IdentityRole();
                role.Name = "Employee";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Customer"))
            {
                var role = new IdentityRole();
                role.Name = "Customer";
                roleManager.Create(role);
            }

            var CheckForUser = UserManager.FindByEmail("stevan.camp12@gmail.com");
            if (CheckForUser == null)
            {
                var user = new ApplicationUser();
                user.UserName = "Stevan";
                user.Email = "Stevan.Camp12@gmail.com";
                user.EmailConfirmed = true;
                string userPWD = "OKCgov1!";
                var chkUser = UserManager.Create(user, userPWD);
                if (chkUser.Succeeded)
                {
                    var resultl = UserManager.AddToRole(user.Id, "WebAdmin");
                }
            }

            var CheckforClint = UserManager.FindByEmail("clint@simplecureok.com");
            if (CheckforClint == null)
            {
                var user = new ApplicationUser();
                user.UserName = "Clint";
                user.Email = "clint@simplecureok.com";
                user.EmailConfirmed = true;
                string userPWD = "tibet33";
                var chkUser = UserManager.Create(user, userPWD);
                if (chkUser.Succeeded)
                {
                    var addroleemployee = UserManager.AddToRole(user.Id, "Employee");
                    var addroleowner = UserManager.AddToRole(user.Id, "Owner");
                }
            }


            var CheckforJenn = UserManager.FindByEmail("jennifer@simplecureok.com");
            if (CheckforJenn == null)
            {
                var user = new ApplicationUser();
                user.UserName = "Jennifer";
                user.Email = "jennifer@simplecureok.com";
                user.EmailConfirmed = true;
                string userPWD = "Password";
                var chkUser = UserManager.Create(user, userPWD);
                if (chkUser.Succeeded)
                {
                    var addroleemployee = UserManager.AddToRole(user.Id, "Employee");
                    var addroleowner = UserManager.AddToRole(user.Id, "Owner");
                }
            }
            var CheckforMike = UserManager.FindByEmail("michael@simplecureok.com");
            if (CheckforMike == null)
            {
                var user = new ApplicationUser();
                user.UserName = "Michael";
                user.Email = "michael@simplecureok.com";
                user.EmailConfirmed = true;
                string userPWD = "Password";
                var chkUser = UserManager.Create(user, userPWD);
                if (chkUser.Succeeded)
                {
                    var addroleemployee = UserManager.AddToRole(user.Id, "Employee");
                    var addroleowner = UserManager.AddToRole(user.Id, "Owner");
                }
            }
        }
    }
}
