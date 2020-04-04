using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTMusic.DataAccess.EFContexts;
using UTMusic.DataAccess.Entities;
using UTMusic.DataAccess.Interfaces;

namespace UTMusic.DataAccess.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly MusicContext db;
        private IRepository<Song> songRepository;
        private IRepository<User> userRepository;
        public EFUnitOfWork()
        {
            db = new MusicContext();
        }
        public EFUnitOfWork(string connectionString)
        {
            db = new MusicContext(connectionString);
        }
        public IRepository<Song> Songs {
            get {
                if (songRepository == null)
                    songRepository = new SongRepository(db);
                return songRepository;
            }
        }

        public IRepository<User> Users {
            get {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
