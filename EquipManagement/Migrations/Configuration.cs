namespace EquipManagement.Migrations
{
    using EquipManagement.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using EquipManagement;
    internal sealed class Configuration : DbMigrationsConfiguration<EquipManagement.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(EquipManagement.Models.ApplicationDbContext context)
        {

            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            if (!RoleManager.RoleExists("Admin"))
            {
                RoleManager.Create(new IdentityRole("Admin"));
                RoleManager.Create(new IdentityRole("Teacher"));
                
            }
            if (UserManager.FindByName("admin@admin.com")==null) {
                var user = new ApplicationUser { Name = "π‹¿Ì‘±", UserName = "admin@admin.com", Email = "admin@admin.com" };
                UserManager.Create(user, "administrator");
                UserManager.AddToRole(UserManager.FindByName(user.UserName).Id, "Admin");
            }
            //}
            
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
