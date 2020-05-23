using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UTMusic.DataAccess.EFContexts;
using UTMusic.DataAccess.Entities;
using UTMusic.DataAccess.Interfaces;

namespace UTMusic.DataAccess.Repositories
{
    internal class IdNumberRepository : IRepository<IdNumber, int>
    {
        private MusicContext db { get; }
        public IdNumberRepository(MusicContext db)
        {
            this.db = db;
        }
        public void Create(IdNumber item)
        {
            db.IdNumbers.Add(item);
            db.SaveChanges();
        }
        public IEnumerable<IdNumber> GetAll()
        {
            return db.IdNumbers.ToList();
        }
        public IdNumber Get(int id)
        {
            return db.IdNumbers.Find(id);
        }
        public IEnumerable<IdNumber> Find(Func<IdNumber, bool> predicate)
        {
            return db.IdNumbers.Where(predicate).ToList();
        }
        public void Update(IdNumber item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
        public void DeleteById(int id)
        {
            var idNumber = Get(id);
            Delete(idNumber);
        }
        public void Delete(IdNumber idNumber)
        {
            if (idNumber != null)
            {
                db.IdNumbers.Remove(idNumber);
            }
        }
    }
}