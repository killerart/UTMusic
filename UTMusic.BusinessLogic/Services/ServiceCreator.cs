using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTMusic.BusinessLogic.Services;
using UTMusic.DataAccess.Repositories;

namespace UTMusic.BusinessLogic.Interfaces
{
    public class ServiceCreator : IServiceCreator
    {
        public IUserService CreateUserService(string connection)
        {
            return new UserService(new UnitOfWork(connection));
        }
        public IMusicService CreateMusicService(string connection)
        {
            return new MusicService(new UnitOfWork(connection));
        }
    }
}
