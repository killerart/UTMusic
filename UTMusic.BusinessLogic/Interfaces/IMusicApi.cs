using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using UTMusic.BusinessLogic.DataTransfer;
using UTMusic.BusinessLogic.Infrastructure;

namespace UTMusic.BusinessLogic.Interfaces
{
    public interface IMusicApi
    {
        IEnumerable<SongDTO> GetSongs();
        void AddSong(SongDTO songDTO);
        IEnumerable<SongDTO> SearchSongs(string searchValue, IEnumerable<SongDTO> songs);
        IEnumerable<SongDTO> SearchSongs(string searchValue);
        OperationResult SaveSongToDisk(HttpPostedFileBase file, string directory, out SongDTO songDTO);
    }
}
