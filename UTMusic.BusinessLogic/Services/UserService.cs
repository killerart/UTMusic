using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTMusic.BusinessLogic.DataTransfer;
using UTMusic.BusinessLogic.Infrastructure;
using UTMusic.BusinessLogic.Interfaces;
using UTMusic.DataAccess.Entities;
using UTMusic.DataAccess.Interfaces;
using UTMusic.DataAccess.Repositories;

namespace UTMusic.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork Database { get; set; }
        public UserService(IUnitOfWork database) => Database = database;

        public void AddNewSong(ref UserDTO userDTO, SongDTO songDTO)
        {
            var user = Database.Users.Get(userDTO.Id);
            if (user != null)
            {
                user.Songs.Add(new Song { Name = songDTO.Name, FileName = songDTO.FileName });
                Database.Save();
                user.OrderOfSongs.Add(new IdNumber { SongId = Database.Songs.Find(s => s.FileName == songDTO.FileName).First().Id });
                Database.Save();
            }
            userDTO = UserToUserDTO(user);
        }

        public void AddExistingSong(ref UserDTO userDTO, int songId)
        {
            var song = Database.Songs.Get(songId);
            var user = Database.Users.Get(userDTO.Id);
            if (song != null && user != null)
            {
                user.Songs.Add(song);
                user.OrderOfSongs.Add(new IdNumber { SongId = songId });
                Database.Save();
                userDTO = UserToUserDTO(user);
            }
        }

        public void DeleteSong(ref UserDTO userDTO, int songId)
        {
            var user = Database.Users.Get(userDTO.Id);
            var song = user.Songs.FirstOrDefault(s => s.Id == songId);
            if (song != null && user != null)
            {
                user.Songs.Remove(song);
                IdNumber idNumber = user.OrderOfSongs.FirstOrDefault(i => i.SongId == songId);
                user.OrderOfSongs.Remove(idNumber);
                Database.Save();
                userDTO = UserToUserDTO(user);
            }
        }

        public OperationResult Authenticate(UserDTO userDTO)
        {
            User user = Database.Users.Find(u => u.Email == userDTO.Email && u.Password == userDTO.Password).FirstOrDefault();
            if (user != null)
            {
                return new OperationResult(true, user.Id.ToString(), "");
            }
            return new OperationResult(false, "Incorrect login data", "");
        }
        public IEnumerable<OperationResult> Create(UserDTO userDTO)
        {
            var registerResults = new List<OperationResult>();
            User userByEmail = Database.Users.Find(u => u.Email == userDTO.Email).FirstOrDefault();
            User userByName = Database.Users.Find(u => u.Name == userDTO.Name).FirstOrDefault();
            if (userByEmail == null && userByName == null)
            {
                Database.Users.Create(
                    new User
                    {
                        Email = userDTO.Email,
                        Name = userDTO.Name,
                        Password = userDTO.Password
                    }
                );
                Database.Save();
                registerResults.Add(new OperationResult(true, "", ""));
            }
            else
            {
                if (userByEmail != null)
                {
                    registerResults.Add(new OperationResult(false, "User with such E-Mail already exists", "Email"));
                }
                if (userByName != null)
                {
                    registerResults.Add(new OperationResult(false, "User with such Name already exists", "Name"));
                }
            }
            return registerResults;
        }
        public UserDTO GetUser(int id)
        {
            var user = Database.Users.Get(id);
            return UserToUserDTO(user);
        }
        private UserDTO UserToUserDTO(User user)
        {
            if (user == null)
                return null;
            var userDTO = new UserDTO { Id = user.Id, Email = user.Email, Name = user.Name, Password = user.Password };
            userDTO.Songs = user.GetOrderedSongs().ConvertAll(s => new SongDTO { Id = s.Id, Name = s.Name, FileName = s.FileName });
            return userDTO;
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
