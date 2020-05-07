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
        public IRepository<Song> Songs { get; }
        public IRepository<ClientProfile> ClientProfiles { get; }
        public ApplicationUserManager UserManager { get; }
        public ApplicationRoleManager RoleManager { get; }
        public UnitOfWork(string connectionString)
        {
            db = new MusicContext(connectionString);
            Songs = new SongRepository(db);
            UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            RoleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
            ClientProfiles = new ClientManager(db);
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
