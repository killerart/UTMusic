using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UTMusic.DataAccess.Entities;

namespace UTMusic.DataAccess.EFContexts
{
    public class MusicContext : DbContext
    {
        public DbSet<Song> Songs { get; set; }
        public DbSet<User> Users { get; set; }
        public MusicContext()
            : base() { }
        public MusicContext(string connectionString)
            : base(connectionString) { }
    }
}