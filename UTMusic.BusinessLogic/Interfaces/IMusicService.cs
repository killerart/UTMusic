using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTMusic.BusinessLogic.DataTransfer;

namespace UTMusic.BusinessLogic.Interfaces
{
    public interface IMusicService
    {
        IEnumerable<SongDTO> GetSongs();
        void AddSong(SongDTO songDTO);
        bool FileExists(string fileName);
    }
}
