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
    public class SongRepository : IRepository<Song, int>
    {
        private readonly MusicContext db;

        public SongRepository(MusicContext context) => db = context;

        public IEnumerable<Song> GetAll() => db.Songs;

        public Song Get(int id) => db.Songs.Find(id);

        public void Create(Song song) => db.Songs.Add(song);

        public void Update(Song song) => db.Entry(song).State = EntityState.Modified;

        public IEnumerable<Song> Find(Func<Song, bool> predicate) => db.Songs.Where(predicate).ToList();

        public void DeleteById(int id)
        {
            Song song = Get(id);
            Delete(song);
        }
        public void Delete(Song song)
        {
            if (song != null)
            {
                var idNumbers = db.IdNumbers.Where(i => i.Song.Id == song.Id);
                db.IdNumbers.RemoveRange(idNumbers);
                db.Songs.Remove(song);
            }
        }
    }
}
