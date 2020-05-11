using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTMusic.DataAccess.EFContexts;
using UTMusic.DataAccess.Entities;
using UTMusic.DataAccess.Interfaces;

namespace UTMusic.DataAccess.Repositories
{
    public class IdNumberRepository : IRepository<IdNumber, int>
    {
        private readonly MusicContext db;

        public IdNumberRepository(MusicContext context) => db = context;

        public IEnumerable<IdNumber> GetAll() => db.IdNumbers;

        public IdNumber Get(int id) => db.IdNumbers.Find(id);

        public void Create(IdNumber idNumber) => db.IdNumbers.Add(idNumber);

        public void Update(IdNumber idNumber) => db.Entry(idNumber).State = EntityState.Modified;

        public IEnumerable<IdNumber> Find(Func<IdNumber, bool> predicate) => db.IdNumbers.Where(predicate).ToList();

        public void DeleteById(int id)
        {
            IdNumber idNumber = Get(id);
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
