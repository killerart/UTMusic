using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using UTMusic.Data.Entities;

namespace UTMusic.BusinessLogic.Interfaces
{
    /// <summary>
    /// Репозиторий с юзерами
    /// </summary>
    public interface IUsersRepository
    {
        /// <summary>
        /// Получить всех пользователей из базы данных
        /// </summary>
        /// <returns>Массив List со всеми пользователями</returns>
        ICollection<User> GetAllUsers();
        /// <summary>
        /// Получить текущего залогиненного пользователя
        /// </summary>
        /// <param name="contoller">Контроллер, в котором вызывается метод</param>
        /// <returns>Пользователь или null, если пользователь не вошел в аккаунт</returns>
        User GetCurrentUser(Controller contoller);
        /// <summary>
        /// Найти пользователя по ID
        /// </summary>
        /// <param name="id">ID пользователя</param>
        /// <returns>Пользователь или null, если нет пользователя с таким ID</returns>
        User GetUserById(int id);
        /// <summary>
        /// Найти пользователя по имени
        /// </summary>
        /// <param name="id">Имя пользователя</param>
        /// <returns>Пользователь или null, если нет пользователя с таким именем</returns>
        User GetUserByName(string name);
        /// <summary>
        /// Найти пользователя по почте
        /// </summary>
        /// <param name="id">Почта пользователя</param>
        /// <returns>Пользователь или null, если нет пользователя с такой почтой</returns>
        User GetUserByEmail(string email);
        /// <summary>
        /// Сохранить нового пользователя в базе данных или обновить существующего
        /// </summary>
        /// <param name="song">Пользователь, которого надо сохранить</param>
        /// <returns>Сохраненный пользователь</returns>
        User SaveUser(User user);
        /// <summary>
        /// Удалить пользователя из базы данных
        /// </summary>
        /// <param name="song">Пользователь, которого надо удалить</param>
        void DeleteUser(User user);
    }
}
