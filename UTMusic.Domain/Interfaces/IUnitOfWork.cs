using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTMusic.DataAccess.Entities;
using UTMusic.DataAccess.Identity;

namespace UTMusic.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Song> Songs { get; }
        IRepository<ClientProfile> ClientProfiles { get; }
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }
        void Save();
        Task SaveAsync();
    }
}
