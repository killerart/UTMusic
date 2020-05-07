using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using UTMusic.BusinessLogic.DataTransfer;

namespace UTMusic.BusinessLogic.Interfaces
{
    public interface IMusicService
    {
        IEnumerable<SongDTO> GetSongs();
        void AddSong(SongDTO songDTO);
        void RemoveSong(int songId, string directory);
        IEnumerable<SongDTO> SearchSongs(string searchValue, IEnumerable<SongDTO> songs);
        IEnumerable<SongDTO> SearchSongs(string searchValue);
        bool SaveSongToDisk(HttpPostedFileBase file, string directory, out SongDTO songDTO);
    }
}
