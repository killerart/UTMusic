﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTMusic.DataAccess.EFContexts;
using UTMusic.DataAccess.Entities;
using UTMusic.DataAccess.Identity;
using UTMusic.DataAccess.Interfaces;

namespace UTMusic.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MusicContext db;
        public IRepository<Song, int> Songs { get; }
        public IRepository<UserProfile, string> UserProfiles { get; }
        public IRepository<IdNumber, int> IdNumbers { get; }
        public ApplicationUserManager UserManager { get; }
        public RoleManager<IdentityRole> RoleManager { get; }
        public UnitOfWork(string connectionString)
        {
            db = new MusicContext(connectionString);
            Songs = new SongRepository(db);
            UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            UserProfiles = new UserProfileRepository(db);
            IdNumbers = new IdNumberRepository(db);
        }
        public void Save()
        {
            db.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    UserManager.Dispose();
                    RoleManager.Dispose();
                }
                disposed = true;
            }
        }
    }
}
