using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<OperationResult>> Create(UserDTO userDTO)
        {
            List<OperationResult> operationResults = new List<OperationResult>();
            ApplicationUser userByMail = await Database.UserManager.FindByEmailAsync(userDTO.Email);
            ApplicationUser userByName = await Database.UserManager.FindByNameAsync(userDTO.UserName);
            if (userByMail == null && userByName == null)
            {
                var user = new ApplicationUser { Email = userDTO.Email, UserName = userDTO.UserName };
                var result = await Database.UserManager.CreateAsync(user, userDTO.Password);
                if (result.Errors.Count() > 0)
                    operationResults.Add(new OperationResult(false, result.Errors.FirstOrDefault(), ""));
                else
                {
                    // добавляем роль
                    await Database.UserManager.AddToRoleAsync(user.Id, userDTO.Role);
                    // создаем профиль клиента
                    UserProfile userProfile = new UserProfile { Id = user.Id };
                    Database.UserProfiles.Create(userProfile);
                    await Database.SaveAsync();
                    operationResults.Add(new OperationResult(true, "Registration succeded", ""));
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
            if (user != null)
                claim = await Database.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }
        public async Task SetInitialData(UserDTO adminDTO, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new IdentityRole { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDTO);
        }
        private UserDTO UserToUserDTO(ApplicationUser user)
        {
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
                user.OrderOfSongs.Add(new IdNumber { SongId = song.Id });
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
                user.OrderOfSongs.Add(new IdNumber { SongId = songId });
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
                IdNumber idNumber = user.OrderOfSongs.FirstOrDefault(i => i.SongId == songId);
                user.OrderOfSongs.Remove(idNumber);
                Database.IdNumbers.Delete(idNumber);
                Database.Save();
            }
        }
        public UserDTO GetUser(string name)
        {
            return UserToUserDTO(Database.UserManager.FindByName(name));
        }
    }
}
