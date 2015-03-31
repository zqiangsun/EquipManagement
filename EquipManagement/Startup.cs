using EquipManagement.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EquipManagement.Startup))]
namespace EquipManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var context = new ApplicationDbContext();
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            if (!RoleManager.RoleExists("Admin"))
            {
                RoleManager.Create(new IdentityRole("Admin"));
                RoleManager.Create(new IdentityRole("Teacher"));

            }
            if (UserManager.FindByName("admin@admin.com") == null)
            {
                var user = new ApplicationUser { Name = "管理员", UserName = "admin@admin.com", Email = "admin@admin.com" };
                UserManager.Create(user, "administrator");
                UserManager.AddToRole(UserManager.FindByName(user.UserName).Id, "Admin");
            }
            ConfigureAuth(app);
        }
    }
}
