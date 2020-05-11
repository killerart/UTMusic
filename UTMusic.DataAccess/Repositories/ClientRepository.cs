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
    public class ClientRepository : IRepository<ClientProfile, string>
    {
        private MusicContext db { get; }
        public ClientRepository(MusicContext db)
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
        public ClientProfile Get(string id)
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
        public void DeleteById(string id)
        {
            var clientProfile = Get(id);
            Delete(clientProfile);
        }
        public void Delete(ClientProfile clientProfile)
        {
            if (clientProfile != null)
            {
                foreach (var idNumber in clientProfile.OrderOfSongs)
                {
                    db.IdNumbers.Remove(idNumber);
                }
                db.ClientProfiles.Remove(clientProfile);
            }
        }
    }
}
