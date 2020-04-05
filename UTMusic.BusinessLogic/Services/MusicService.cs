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

        public bool FileExists(string fileName)
        {
            return Database.Songs.Find(s => s.FileName == fileName).FirstOrDefault() != null;
        }

        public IEnumerable<SongDTO> GetSongs()
        {
            return Database.Songs.GetAll()?.Reverse().ToList().ConvertAll(s => new SongDTO { Id = s.Id, FileName = s.FileName, Name = s.Name });
        }
    }
}
