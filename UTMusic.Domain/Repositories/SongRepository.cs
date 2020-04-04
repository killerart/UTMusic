using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTMusic.DataAccess.Interfaces;
using UTMusic.DataAccess.EFContexts;
using UTMusic.DataAccess.Entities;

namespace UTMusic.DataAccess.Repositories
{
    public class SongRepository : IRepository<Song>
    {
        private readonly MusicContext db;

        public SongRepository(MusicContext context)
        {
            db = context;
        }

        public IEnumerable<Song> GetAll()
        {
            return db.Songs;
        }

        public Song Get(int id)
        {
            return db.Songs.Find(id);
        }

        public void Create(Song song)
        {
            db.Songs.Add(song);
        }

        public void Update(Song song)
        {
            db.Entry(song).State = EntityState.Modified;
        }

        public IEnumerable<Song> Find(Func<Song, Boolean> predicate)
        {
            return db.Songs.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Song song = db.Songs.Find(id);
            if (song != null)
                db.Songs.Remove(song);
        }
    }
}
