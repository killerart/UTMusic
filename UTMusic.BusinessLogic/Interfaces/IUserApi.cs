using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UTMusic.BusinessLogic.DataTransfer;
using UTMusic.BusinessLogic.Infrastructure;
using UTMusic.DataAccess.Entities;

namespace UTMusic.BusinessLogic.Interfaces
{
    public interface IUserApi : IDisposable
    {
        Task<IEnumerable<OperationResult>> Create(UserDTO userDTO);
        Task<ClaimsIdentity> Authenticate(UserDTO userDTO);
        Task SetInitialData(UserDTO adminDTO, List<string> roles);
        UserDTO GetUser(string id);
        void AddNewSong(UserDTO userDTO, SongDTO songDTO);
        void AddExistingSong(UserDTO userDTO, int songId);
        void RemoveSong(UserDTO userDTO, int songId);
    }
}
