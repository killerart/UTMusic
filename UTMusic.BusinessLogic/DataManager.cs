using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTMusic.BusinessLogic.Implementations;
using UTMusic.BusinessLogic.Interfaces;

namespace UTMusic.BusinessLogic
{
    public class DataManager
    {
        public ISongsRepository Songs { get; }
        public IUsersRepository Users { get; }
        public DataManager(ISongsRepository songsRepository = null, IUsersRepository usersRepository=null)
        {
            Songs = songsRepository ?? new EFSongsRepository();
            Users = usersRepository ?? new EFUsersRepository();
        }
    }
}
