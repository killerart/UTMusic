using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTMusic.BusinessLogic.Implementations;
using UTMusic.BusinessLogic.Interfaces;
using UTMusic.Data.Entities;

namespace UTMusic.BusinessLogic
{
    /// <summary>
    /// Менеджер репозиториев
    /// </summary>
    public class DataManager
    {
        private MusicContext MusicContext { get; }
        public ISongsRepository Songs { get; }
        public IUsersRepository Users { get; }
        public DataManager(MusicContext musicContext = null)
        {
            Songs = new EFSongsRepository(musicContext);
            Users = new EFUsersRepository(musicContext);
        }
    }
}
