using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTMusic.BusinessLogic.Interfaces;
using UTMusic.Data.Entities;

namespace UTMusic.BusinessLogic.Implementations
{
    /// <summary>
    /// Реализация репозитория песен через Entity Framework
    /// </summary>
    public class EFSongsRepository : ISongsRepository
    {
        /// <summary>
        /// Контекст базы данных с песнями
        /// </summary>
        private MusicContext MusicContext { get; }

        public EFSongsRepository(MusicContext songContext = null)
        {
            MusicContext = songContext ?? new MusicContext();
        }
        public ICollection<Song> GetAllSongs()
        {
            return MusicContext.Songs.ToList();
        }
        public Song GetSongById(int id)
        {
            return MusicContext.Songs.FirstOrDefault(song => song.Id == id);
        }
        public Song GetSongByName(string name)
        {
            return MusicContext.Songs.FirstOrDefault(song => song.Name == name);
        }
        public Song GetSongByFileName(string fileName)
        {
            return MusicContext.Songs.FirstOrDefault(song => song.FileName == fileName);
        }
        public Song SaveSong(Song song)
        {
            if (song.Id == 0)
                MusicContext.Songs.Add(song);
            else
                MusicContext.Entry(song).State = EntityState.Modified;
            MusicContext.SaveChanges();
            return GetSongByFileName(song.FileName);
        }
        public void DeleteSong(Song song)
        {
            MusicContext.Songs.Remove(song);
            MusicContext.SaveChanges();
        }
    }
}
