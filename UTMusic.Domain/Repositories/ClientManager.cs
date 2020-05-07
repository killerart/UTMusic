using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UTMusic.DataAccess.EFContexts;
using UTMusic.DataAccess.Entities;
using UTMusic.DataAccess.Interfaces;

namespace UTMusic.DataAccess.Repositories
{
    public class ClientManager : IRepository<ClientProfile>
    {
        private MusicContext db { get; }
        public ClientManager(MusicContext db)
        {
            this.db = db;
        }
        public void Create(ClientProfile item)
        {
            db.ClientProfiles.Add(item);
            db.SaveChanges();
        }
        public IEnumerable<ClientProfile> GetAll()
        {
            return db.ClientProfiles.ToList();
        }
        public ClientProfile Get(int id)
        {
            return db.ClientProfiles.Find(id);
        }
        public IEnumerable<ClientProfile> Find(Func<ClientProfile, bool> predicate)
        {
            return db.ClientProfiles.Where(predicate).ToList();
        }
        public void Update(ClientProfile item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            var clientProfile = db.ClientProfiles.Find(id);
            if (clientProfile != null)
                db.ClientProfiles.Remove(clientProfile);
        }
    }
}
