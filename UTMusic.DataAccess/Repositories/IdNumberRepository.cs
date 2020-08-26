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
        private readonly MusicContext _db;
        public IdNumberRepository(MusicContext db)
        {
            this._db = db;
        }
        public void Create(IdNumber item)
        {
            _db.IdNumbers.Add(item);
            _db.SaveChanges();
        }
        public IEnumerable<IdNumber> GetAll()
        {
            return _db.IdNumbers.ToList();
        }
        public IdNumber Get(int id)
        {
            return _db.IdNumbers.Find(id);
        }
        public IEnumerable<IdNumber> Find(Func<IdNumber, bool> predicate)
        {
            return _db.IdNumbers.Where(predicate).ToList();
        }
        public void Update(IdNumber item)
        {
            _db.Entry(item).State = EntityState.Modified;
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
                _db.IdNumbers.Remove(idNumber);
            }
        }
    }
}