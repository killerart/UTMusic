using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UTMusic.DataAccess.Entities;
using UTMusic.DataAccess.Identity;
using UTMusic.DataAccess.Interfaces;
using UTMusic.DataAccess.Repositories;

namespace UTMusic.DataAccess.EFContexts
{
    public class MusicContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Song> Songs { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<IdNumber> IdNumbers { get; set; }
        public MusicContext(string connectionString)
            : base(connectionString) { }
        static MusicContext()
        {
            Database.SetInitializer(new AdminInitializer());
        }
    }

    public class AdminInitializer : CreateDatabaseIfNotExists<MusicContext>
    {
        protected override void Seed(MusicContext context)
        {
            base.Seed(context);
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userProfiles = new UserProfileRepository(context);
            var roles = new List<string> { "user", "admin" };
            foreach (string roleName in roles)
            {
                var role = new IdentityRole { Name = roleName };
                roleManager.Create(role);
            }
            var user = new ApplicationUser
            {
                Email = "admin@mail.ru",
                UserName = "admin",
            };
            var result = userManager.Create(user, "Admin12345");
            user.EmailConfirmed = true;
            if (result.Succeeded)
            {
                // добавляем роль
                userManager.AddToRole(user.Id, "admin");
                // создаем профиль клиента
                UserProfile userProfile = new UserProfile { Id = user.Id };
                userProfiles.Create(userProfile);
                context.SaveChanges();
            }
        }
    }
}