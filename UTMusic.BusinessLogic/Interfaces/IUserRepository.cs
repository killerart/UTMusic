using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using UTMusic.Data.Entities;

namespace UTMusic.BusinessLogic.Interfaces
{
    public interface IUsersRepository
    {
        List<User> GetAllUsers();
        User GetCurrentUser(Controller contoller);
        User GetUserById(int id);
        User GetUserByName(string name);
        User GetUserByEmail(string email);
        User SaveUser(User user);
        void DeleteUser(User user);
    }
}
