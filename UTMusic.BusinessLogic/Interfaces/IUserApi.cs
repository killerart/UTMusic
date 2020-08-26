using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UTMusic.BusinessLogic.DataTransfer;
using UTMusic.BusinessLogic.Infrastructure;
using UTMusic.DataAccess.Entities;

namespace UTMusic.BusinessLogic.Interfaces
{
    public interface IUserApi : IDisposable
    {
        Task<IEnumerable<OperationResult>> Create(UserDTO userDTO, HttpRequestBase request, UrlHelper url);
        Task<ClaimsIdentity> Authenticate(UserDTO userDTO);
        UserDTO GetUser(string id);
        Task<OperationResult> ConfirmEmail(string userId, string code);
        void AddNewSong(UserDTO userDTO, SongDTO songDTO);
        void AddExistingSong(UserDTO userDTO, int songId);
        void RemoveSong(UserDTO userDTO, int songId);
    }
}
