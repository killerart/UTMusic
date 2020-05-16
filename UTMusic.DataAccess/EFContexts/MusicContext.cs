using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UTMusic.DataAccess.Entities;

namespace UTMusic.DataAccess.EFContexts
{
    public class MusicContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Song> Songs { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<IdNumber> IdNumbers { get; set; }
        public MusicContext(string connectionString)
            : base(connectionString) { }
    }
}