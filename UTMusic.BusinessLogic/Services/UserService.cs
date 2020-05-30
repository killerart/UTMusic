using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject.Activation;
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
using UTMusic.BusinessLogic.Interfaces;
using UTMusic.DataAccess.Entities;
using UTMusic.DataAccess.Interfaces;

namespace UTMusic.BusinessLogic.Services
{
    public class UserService : Service, IUserApi
    {
        public UserService(IUnitOfWork database) : base(database)
        {
        }

        public async Task<IEnumerable<OperationResult>> Create(UserDTO userDTO, HttpRequestBase request, UrlHelper url)
        {
            List<OperationResult> operationResults = new List<OperationResult>();
            ApplicationUser userByMail = await Database.UserManager.FindByEmailAsync(userDTO.Email);
            ApplicationUser userByName =  await Database.UserManager.FindByNameAsync(userDTO.UserName);
            if (userByMail == null && userByName == null)
            {
                var user = new ApplicationUser { Email = userDTO.Email, UserName = userDTO.UserName };
                var result = await Database.UserManager.CreateAsync(user, userDTO.Password);
                if (result.Succeeded)
                {
                    // добавляем роль
                    await Database.UserManager.AddToRoleAsync(user.Id, userDTO.Role);
                    // создаем профиль клиента
                    UserProfile userProfile = new UserProfile { Id = user.Id };
                    Database.UserProfiles.Create(userProfile);
                    Database.Save();
                    // генерируем токен для подтверждения регистрации
                    var code = await Database.UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // создаем ссылку для подтверждения
                    var callbackUrl = url.Action("ConfirmEmail", "Account", new { userId = user.Id, code },
                               protocol: request.Url.Scheme);
                    // отправка письма
                    await Database.UserManager.SendEmailAsync(user.Id, "Confirm e-mail",
                               "Follow this link to complete the registration: <a href=\""
                                                               + callbackUrl + "\">Confirm E-mail</a>");
                    operationResults.Add(new OperationResult(true, "Registration succeded", ""));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        operationResults.Add(new OperationResult(false, error, ""));
                    }
                }
            }
            else
            {
                if (userByMail != null)
                    operationResults.Add(new OperationResult(false, "User with such E-mail already exists", "Email"));
                if (userByName != null)
                    operationResults.Add(new OperationResult(false, "User with such Username already exists", "Username"));
            }
            return operationResults;
        }
        public async Task<ClaimsIdentity> Authenticate(UserDTO userDTO)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            ApplicationUser user = await Database.UserManager.FindAsync(userDTO.UserName, userDTO.Password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null && user.EmailConfirmed)
            {
                    claim = await Database.UserManager.CreateIdentityAsync(user,
                                                DefaultAuthenticationTypes.ApplicationCookie);
            }
            return claim;
        }

        public async Task<OperationResult> ConfirmEmail(string userId, string code)
        {
            var result = await Database.UserManager.ConfirmEmailAsync(userId, code);
            return new OperationResult(result.Succeeded, result.Errors.FirstOrDefault(), "");
        }

        private UserDTO UserToUserDTO(ApplicationUser user)
        {
            if (user == null)
                return null;
            var userDTO = new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Role = Database.UserManager.GetRoles(user.Id).FirstOrDefault()
            };
            userDTO.Songs = user.ClientProfile.GetOrderedSongs().ConvertAll(s => new SongDTO { Id = s.Id, Name = s.Name, FileName = s.FileName });
            return userDTO;
        }
        public void AddNewSong(UserDTO userDTO, SongDTO songDTO)
        {
            var user = Database.UserProfiles.Get(userDTO.Id);
            if (user != null)
            {
                var song = new Song { Name = songDTO.Name, FileName = songDTO.FileName };
                user.Songs.Add(song);
                Database.Save();
                user.OrderOfSongs.Add(new IdNumber { Song = song });
                Database.Save();
            }
        }
        public void AddExistingSong(UserDTO userDTO, int songId)
        {
            var song = Database.Songs.Get(songId);
            var user = Database.UserProfiles.Get(userDTO.Id);
            if (song != null && user != null)
            {
                user.Songs.Add(song);
                user.OrderOfSongs.Add(new IdNumber { Song = song });
                Database.Save();
            }
        }
        public void RemoveSong(UserDTO userDTO, int songId)
        {
            var user = Database.UserProfiles.Get(userDTO.Id);
            var song = user.Songs.FirstOrDefault(s => s.Id == songId);
            if (song != null && user != null)
            {
                user.Songs.Remove(song);
                var idNumber = user.OrderOfSongs.FirstOrDefault(i => i.Song == song);
                Database.IdNumbers.DeleteById(idNumber.Id);
                Database.Save();
            }
        }
        public UserDTO GetUser(string name)
        {
            return UserToUserDTO(Database.UserManager.FindByName(name));
        }
    }
}
