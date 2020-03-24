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
    public class EFSongsRepository : ISongsRepository
    {
        private SongContext SongContext { get; }

        public EFSongsRepository(SongContext songContext = null)
        {
            SongContext = songContext ?? new SongContext();
        }
        public List<Song> GetAllSongs()
        {
            return SongContext.Songs.ToList();
        }
        public Song GetSongById(int id)
        {
            return SongContext.Songs.FirstOrDefault(song => song.Id == id);
        }
        public Song GetSongByName(string name)
        {
            return SongContext.Songs.FirstOrDefault(song => song.Name == name);
        }
        public Song GetSongByFileName(string fileName)
        {
            return SongContext.Songs.FirstOrDefault(song => song.FileName == fileName);
        }
        public Song SaveSong(Song song)
        {
            if (song.Id == 0)
                SongContext.Songs.Add(song);
            else
                SongContext.Entry(song).State = EntityState.Modified;
            SongContext.SaveChanges();
            return GetSongByFileName(song.FileName);
        }
        public void DeleteSong(Song song)
        {
            SongContext.Songs.Remove(song);
            SongContext.SaveChanges();
        }
    }
}
