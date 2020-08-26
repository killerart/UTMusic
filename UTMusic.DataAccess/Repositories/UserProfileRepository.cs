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
        private readonly MusicContext _db;
        public UserProfileRepository(MusicContext db)
        {
            _db = db;
        }
        public void Create(UserProfile item)
        {
            _db.UserProfiles.Add(item);
            _db.SaveChanges();
        }
        public IEnumerable<UserProfile> GetAll()
        {
            return _db.UserProfiles.ToList();
        }
        public UserProfile Get(string id)
        {
            return _db.UserProfiles.Find(id);
        }
        public IEnumerable<UserProfile> Find(Func<UserProfile, bool> predicate)
        {
            return _db.UserProfiles.Where(predicate).ToList();
        }
        public void Update(UserProfile item)
        {
            _db.Entry(item).State = EntityState.Modified;
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
                _db.IdNumbers.RemoveRange(clientProfile.OrderOfSongs);
                _db.UserProfiles.Remove(clientProfile);
            }
        }
    }
}
