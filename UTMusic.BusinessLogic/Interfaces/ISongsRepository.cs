using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTMusic.Data.Entities;

namespace UTMusic.BusinessLogic.Interfaces
{
    public interface ISongsRepository
    {
        List<Song> GetAllSongs();
        Song GetSongById(int songId);
        Song GetSongByName(string songName);
        Song SaveSong(Song song);
        void DeleteSong(Song song);
    }
}
