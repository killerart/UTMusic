using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using UTMusic.BusinessLogic.DataTransfer;
using UTMusic.BusinessLogic.Infrastructure;
using UTMusic.BusinessLogic.Interfaces;
using UTMusic.DataAccess.Entities;
using UTMusic.DataAccess.Interfaces;

namespace UTMusic.BusinessLogic.Services
{
    public class MusicService : Service, IMusicApi
    {
        public MusicService(IUnitOfWork database) : base(database)
        {
        }

        public IEnumerable<SongDTO> GetSongs()
        {
            return Database.Songs.GetAll()?.Reverse().ToList()
                .ConvertAll(s => new SongDTO { Id = s.Id, FileName = s.FileName, Name = s.Name });
        }
        public void AddSong(SongDTO songDTO)
        {
            Database.Songs.Create(new Song { Name = songDTO.Name, FileName = songDTO.FileName });
            Database.Save();
        }
        public OperationResult SaveSongToDisk(HttpPostedFileBase file, string directory, out SongDTO songDTO)
        {
            songDTO = null;
            if (file != null)
            {
                var extention = Path.GetExtension(file.FileName);
                if (extention == ".mp3")
                {
                    var songName = Path.GetFileNameWithoutExtension(file.FileName);
                    var fileName = songName;
                    while (FileExists(fileName))
                    {
                        fileName += "1";
                    }
                    if (!Directory.Exists(directory))
                        Directory.CreateDirectory(directory);
                    var fileSavePath = directory + "/" +
                      fileName + extention;
                    file.SaveAs(fileSavePath);

                    songDTO = new SongDTO { Name = songName, FileName = fileName };
                    return new OperationResult(true, "", "");
                }
                return new OperationResult(false, "File is not in .mp3 format", "");
            }
            return new OperationResult(false, "File upload failed", "");
        }
        private bool FileExists(string fileName) => Database.Songs.Find(s => s.FileName == fileName).FirstOrDefault() != null;
        public IEnumerable<SongDTO> SearchSongs(string searchValue, IEnumerable<SongDTO> songs)
        {
            if (songs != null && !string.IsNullOrEmpty(searchValue))
            {
                Func<SongDTO, bool> searchFunc = (song => song.Name.IndexOf(searchValue, StringComparison.InvariantCultureIgnoreCase) != -1);
                return songs?.Where(searchFunc);
            }
            return songs;
        }
        public IEnumerable<SongDTO> SearchSongs(string searchValue)
        {
            return SearchSongs(searchValue, GetSongs());
        }
    }
}
