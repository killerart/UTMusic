using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using UTMusic.BusinessLogic.Interfaces;
using UTMusic.Data.Entities;

namespace UTMusic.BusinessLogic.Implementations
{
    /// <summary>
    /// Реализация репозитория пользователей через Entity Framework
    /// </summary>
    public class EFUsersRepository : IUsersRepository
    {
        private UserContext UserContext { get; }
        public EFUsersRepository(UserContext userContext = null)
        {
            UserContext = userContext ?? new UserContext();
        }
        public List<User> GetAllUsers()
        {
            return UserContext.Users.ToList();
        }
        public User GetCurrentUser(Controller controller)
        {
            return controller.User.Identity.IsAuthenticated ? GetUserByName(controller.User.Identity.Name) : null;
        }
        public User GetUserById(int id)
        {
            return UserContext.Users.FirstOrDefault(user => user.Id == id);
        }
        public User GetUserByEmail(string email)
        {
            return UserContext.Users.FirstOrDefault(user => user.Email == email);

        }
        public User GetUserByName(string name)
        {
            return UserContext.Users.FirstOrDefault(user => user.Name == name);
        }
        public User SaveUser(User user)
        {
            if (user.Id == 0)
                UserContext.Users.Add(user);
            else
                UserContext.Entry(user).State = EntityState.Modified;
            UserContext.SaveChanges();
            return GetUserByName(user.Name);
        }
        public void DeleteUser(User user)
        {
            UserContext.Users.Remove(user);
            UserContext.SaveChanges();
        }
    }
}
