using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTMusic.BusinessLogic.Interfaces;
using UTMusic.DataAccess.Interfaces;

namespace UTMusic.BusinessLogic.Services
{
    public class AdminService : Service, IAdminApi
    {
        public AdminService(IUnitOfWork database) : base(database)
        {
        }

        public void RemoveSong(int songId, string directory)
        {
            var song = Database.Songs.Get(songId);
            var path = directory + "/" + song.FileName + ".mp3";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            Database.Songs.Delete(song);
            Database.Save();
        }
    }
}
