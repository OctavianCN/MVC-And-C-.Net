using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using T_ShirtStore.Models;

[assembly: OwinStartupAttribute(typeof(T_ShirtStore.Startup))]
namespace T_ShirtStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateAdminAndUserRoles();
        }
        private void CreateAdminAndUserRoles()
        {
            var ctx = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(ctx));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ctx));

            // adaugam rolurile pe care le poate avea un utilizator
            // din cadrul aplicatiei
            if (!roleManager.RoleExists("Admin"))
            {
                // adaugam rolul de administrator
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                // se adauga utilizatorul administrator
                var user = new ApplicationUser();
                user.UserName = "admin@admin.com";
                user.Email = "admin@admin.com";

                var adminCreated = userManager.Create(user, "Admin2020!");
                if (adminCreated.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }
            // ATENTIE !!! Pentru proiecte, pentru a adauga un rol nou trebuie sa adaugati secventa:
            if (!roleManager.RoleExists("Helper"))
            {
            // adaugati rolul specific aplicatiei voastre
            var role = new IdentityRole();
            role.Name = "Helper";
            roleManager.Create(role);
            // se adauga utilizatorul
            var user = new ApplicationUser();
            user.UserName = "helper@helper.com";
            user.Email = "helper@helper.com";
                var helperCreated = userManager.Create(user, "Helper2020!");
                if(helperCreated.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Helper");
                }
            }

            if (!roleManager.RoleExists("User"))
            {
                // adaugati rolul specific aplicatiei voastre
                var role = new IdentityRole();
                role.Name = "User";
                roleManager.Create(role);
                // se adauga utilizatorul
                var user = new ApplicationUser();
                user.UserName = "userul@user.com";
                user.Email = "userul@user.com";
                var helperCreated = userManager.Create(user, "User2020!");
                if (helperCreated.Succeeded)
                {
                    userManager.AddToRole(user.Id, "User");
                }
            }
        }
    }
}
