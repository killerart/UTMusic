using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTMusic.BusinessLogic.DataTransfer;
using UTMusic.BusinessLogic.Infrastructure;

namespace UTMusic.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        OperationResult Authenticate(UserDTO userDTO);
        UserDTO GetUser(int id);
        void AddNewSong(ref UserDTO userDTO, SongDTO songDTO);
        void AddExistingSong(ref UserDTO userDTO, int songId);
        void DeleteSong(ref UserDTO userDTO, int songId);
        IEnumerable<OperationResult> Create(UserDTO userDTO);
    }
}
