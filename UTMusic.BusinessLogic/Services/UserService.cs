using Microsoft.AspNet.Identity;
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
    public class UserService : IUserService, IDisposable
    {
        private IUnitOfWork Database { get; set; }
        public UserService(IUnitOfWork database) => Database = database;
        public async Task<IEnumerable<OperationDetails>> Create(UserDTO userDTO)
        {
            List<OperationDetails> operationDetails = new List<OperationDetails>();
            ApplicationUser userByMail = await Database.UserManager.FindByEmailAsync(userDTO.Email);
            ApplicationUser userByName = await Database.UserManager.FindByNameAsync(userDTO.UserName);
            if (userByMail == null && userByName == null)
            {
                var user = new ApplicationUser { Email = userDTO.Email, UserName = userDTO.UserName };
                var result = await Database.UserManager.CreateAsync(user, userDTO.Password);
                if (result.Errors.Count() > 0)
                    operationDetails.Add(new OperationDetails(false, result.Errors.FirstOrDefault(), ""));
                else
                {
                    // добавляем роль
                    await Database.UserManager.AddToRoleAsync(user.Id, userDTO.Role);
                    // создаем профиль клиента
                    ClientProfile clientProfile = new ClientProfile { Id = user.Id, UserName = user.UserName };
                    Database.ClientProfiles.Create(clientProfile);
                    await Database.SaveAsync();
                    operationDetails.Add(new OperationDetails(true, "Registration succeded", ""));
                }
            }
            else
            {
                if (userByMail != null)
                    operationDetails.Add(new OperationDetails(false, "User with such E-mail already exists", "Email"));

                if (userByName != null)
                    operationDetails.Add(new OperationDetails(false, "User with such Username already exists", "Username"));

            }
            return operationDetails;
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
                    role = new ApplicationRole { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDTO);
        }
        public void Dispose()
        {
            Database.Dispose();
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
        public void AddNewSong(ref UserDTO userDTO, SongDTO songDTO)
        {
            var user = Database.UserManager.FindById(userDTO.Id);
            if (user != null)
            {
                var song = new Song { Name = songDTO.Name, FileName = songDTO.FileName };
                user.ClientProfile.Songs.Add(song);
                Database.Save();
                user.ClientProfile.OrderOfSongs.Add(new IdNumber { SongId = song.Id });
                Database.Save();
                userDTO = UserToUserDTO(user);
            }
        }
        public void AddExistingSong(ref UserDTO userDTO, int songId)
        {
            var song = Database.Songs.Get(songId);
            var user = Database.UserManager.FindById(userDTO.Id);
            if (song != null && user != null)
            {
                user.ClientProfile.Songs.Add(song);
                user.ClientProfile.OrderOfSongs.Add(new IdNumber { SongId = songId });
                Database.Save();
                userDTO = UserToUserDTO(user);
            }
        }
        public void RemoveSong(ref UserDTO userDTO, int songId)
        {
            var user = Database.UserManager.FindById(userDTO.Id);
            var song = user.ClientProfile.Songs.FirstOrDefault(s => s.Id == songId);
            if (song != null && user != null)
            {
                user.ClientProfile.Songs.Remove(song);
                IdNumber idNumber = user.ClientProfile.OrderOfSongs.FirstOrDefault(i => i.SongId == songId);
                user.ClientProfile.OrderOfSongs.Remove(idNumber);
                Database.Save();
                userDTO = UserToUserDTO(user);
            }
        }
        public UserDTO GetUser(string name)
        {
            return UserToUserDTO(Database.UserManager.FindByName(name));
        }
    }
}
