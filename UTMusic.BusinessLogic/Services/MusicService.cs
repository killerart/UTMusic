using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using UTMusic.BusinessLogic.DataTransfer;
using UTMusic.BusinessLogic.Interfaces;
using UTMusic.DataAccess.Entities;
using UTMusic.DataAccess.Interfaces;

namespace UTMusic.BusinessLogic.Services
{
    public class MusicService : IMusicService, IDisposable
    {
        private IUnitOfWork Database { get; set; }
        public MusicService(IUnitOfWork database) => Database = database;
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
        public void RemoveSong(int songId, string directory)
        {
            var song = Database.Songs.Get(songId);
            var path = directory + "/" + song.FileName + ".mp3";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            Database.Songs.Delete(songId);
            Database.Save();
        }
        public bool SaveSongToDisk(HttpPostedFileBase file, string directory, out SongDTO songDTO)
        {
            songDTO = null;
            if (file != null)
            {
                var extention = Path.GetExtension(file.FileName);
                if (extention == ".mp3")
                {
                    var songName = Path.GetFileNameWithoutExtension(file.FileName);
                    var fileName = songName;
                    if (FileExists(fileName))
                    {
                        fileName += "1";
                    }
                    if (!Directory.Exists(directory))
                        Directory.CreateDirectory(directory);
                    var fileSavePath = directory + "/" +
                      fileName + extention;
                    file.SaveAs(fileSavePath);

                    songDTO = new SongDTO { Name = songName, FileName = fileName };
                    return true;
                }
            }
            return false;
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
        public void Dispose() => Database.Dispose();
    }
}
