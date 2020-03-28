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
        private MusicContext MusicContext { get; }
        public EFUsersRepository(MusicContext userContext = null)
        {
            MusicContext = userContext ?? new MusicContext();
        }
        public List<User> GetAllUsers()
        {
                return MusicContext.Users.ToList();
        }
        public User GetCurrentUser(Controller controller)
        {
            return controller.User.Identity.IsAuthenticated ? GetUserByName(controller.User.Identity.Name) : null;
        }
        public User GetUserById(int id)
        {
            return MusicContext.Users.FirstOrDefault(user => user.Id == id);
        }
        public User GetUserByEmail(string email)
        {
            return MusicContext.Users.FirstOrDefault(user => user.Email == email);

        }
        public User GetUserByName(string name)
        {
            return MusicContext.Users.FirstOrDefault(user => user.Name == name);
        }
        public User SaveUser(User user)
        {
            if (user.Id == 0)
                MusicContext.Users.Add(user);
            else
                MusicContext.Entry(user).State = EntityState.Modified;
            MusicContext.SaveChanges();
            return GetUserByName(user.Name);
        }
        public void DeleteUser(User user)
        {
            MusicContext.Users.Remove(user);
            MusicContext.SaveChanges();
        }
    }
}
