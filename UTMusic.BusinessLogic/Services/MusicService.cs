using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTMusic.BusinessLogic.DataTransfer;
using UTMusic.BusinessLogic.Interfaces;
using UTMusic.DataAccess.Entities;
using UTMusic.DataAccess.Interfaces;
using UTMusic.DataAccess.Repositories;

namespace UTMusic.BusinessLogic.Services
{
    public class MusicService : IMusicService
    {
        private IUnitOfWork Database { get; set; }
        public MusicService(IUnitOfWork database) => Database = database;

        public void AddSong(SongDTO songDTO)
        {
            Database.Songs.Create(new Song { Name = songDTO.Name, FileName = songDTO.FileName });
            Database.Save();
        }

        public bool FileExists(string fileName) => Database.Songs.Find(s => s.FileName == fileName).FirstOrDefault() != null;

        public IEnumerable<SongDTO> GetSongs() => Database.Songs.GetAll()?.Reverse().ToList()
            .ConvertAll(s => new SongDTO { Id = s.Id, FileName = s.FileName, Name = s.Name });
        public IEnumerable<SongDTO> SearchSongs(IEnumerable<SongDTO> songs, string searchValue)
        {
            if (songs != null && !string.IsNullOrEmpty(searchValue))
            {
                Func<SongDTO, bool> searchFunc = (song) => song.Name.IndexOf(searchValue, StringComparison.InvariantCultureIgnoreCase) != -1;
                return songs?.Where(searchFunc);
            }
            return songs;
        }
        public IEnumerable<SongDTO> SearchSongs(string searchValue)
        {
            return SearchSongs(GetSongs(), searchValue);
        }
    }
}
