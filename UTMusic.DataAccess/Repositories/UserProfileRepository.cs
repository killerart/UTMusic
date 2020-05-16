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
    public class UserProfileRepository : IRepository<UserProfile, string>
    {
        private MusicContext db { get; }
        public UserProfileRepository(MusicContext db)
        {
            this.db = db;
        }
        public void Create(UserProfile item)
        {
            db.UserProfiles.Add(item);
            db.SaveChanges();
        }
        public IEnumerable<UserProfile> GetAll()
        {
            return db.UserProfiles.ToList();
        }
        public UserProfile Get(string id)
        {
            return db.UserProfiles.Find(id);
        }
        public IEnumerable<UserProfile> Find(Func<UserProfile, bool> predicate)
        {
            return db.UserProfiles.Where(predicate).ToList();
        }
        public void Update(UserProfile item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
        public void DeleteById(string id)
        {
            var clientProfile = Get(id);
            Delete(clientProfile);
        }
        public void Delete(UserProfile clientProfile)
        {
            if (clientProfile != null)
            {
                foreach (var idNumber in clientProfile.OrderOfSongs)
                {
                    db.IdNumbers.Remove(idNumber);
                }
                db.UserProfiles.Remove(clientProfile);
            }
        }
    }
}
