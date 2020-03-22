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
        private SongContext songContext;
        public EFSongsRepository(SongContext songContext)
        {
            this.songContext = songContext;
        }
        public List<Song> GetAllSongs()
        {
            return songContext.Songs.ToList();
        }
        public Song GetSongById(int songId)
        {
            return songContext.Songs.FirstOrDefault(song => song.Id == songId);
        }
        public Song GetSongByName(string songName)
        {
            return songContext.Songs.FirstOrDefault(song => song.Name == songName);
        }
        public void SaveSong(Song song)
        {
            if (song.Id == 0)
                songContext.Songs.Add(song);
            else
                songContext.Entry(song).State = EntityState.Modified;
            songContext.SaveChanges();
        }
        public void DeleteSong(Song song) {
            songContext.Songs.Remove(song);
            songContext.SaveChanges();

        }
    }
}
