using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models
{
    public class AppDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleMenager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // создание ролей
            var admin = new IdentityRole { Name = "Administrator" };
            var superUser = new IdentityRole { Name = "SuperUser" };
            var manager = new IdentityRole { Name = "Manager" };

            // Добавляем роли в бд
            roleMenager.Create(admin);
            roleMenager.Create(superUser);
            roleMenager.Create(manager);

            // создаем пользователя создание пользователей администратором
           
        }
    }
}