using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTMusic.DataAccess.Entities;

namespace UTMusic.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Song> Songs { get; }
        IRepository<User> Users { get; }
        void Save();
    }
}
