using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UTMusic.BusinessLogic;
using UTMusic.Data.Entities;
using UTMusic.Web.Models;

namespace UTMusic.Web.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// Менеджер репозиториев
        /// </summary>
        private DataManager DataManager { get; } = new DataManager();
        /// <summary>
        /// Действие страницы логина
        /// </summary>
        /// <returns>Страница регистрации</returns>
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View(new LoginModel());
        }
        /// <summary>
        /// Действие страницы регистрации
        /// </summary>
        /// <returns>Страница регистрации</returns>
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View(new RegisterModel());
        }
        /// <summary>
        /// Обработка формы логина
        /// </summary>
        /// <param name="loginModel">Данные о логине, передаваемые из формы</param>
        /// <returns>
        /// Главная страница, если логин - успешный
        /// Страница логина, если данные логина неверные
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                User user = DataManager.Users.GetUserByEmail(loginModel.Email);
                if (user != null)
                {
                    if (user.Password == loginModel.Password)
                    {
                        FormsAuthentication.SetAuthCookie(user.Name, loginModel.Remember);
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "Incorrect login data");
            }
            return View(loginModel);
        }
        /// <summary>
        /// Обработка формы регистрации
        /// </summary>
        /// <param name="registerModel">Данные о регистрации, передаваемые из формы</param>
        /// <returns>
        /// Страница логина, если регистрация успешная
        /// Страница регистрации, если данные для регистрации уже заняты
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                User userByEmail = DataManager.Users.GetUserByEmail(registerModel.Email);
                User userByName = DataManager.Users.GetUserByName(registerModel.Name);
                if (userByEmail == null && userByName == null)
                {
                    DataManager.Users.SaveUser(
                        new User
                        {
                            Email = registerModel.Email,
                            Name = registerModel.Name,
                            Password = registerModel.Password
                        }
                    );
                    return RedirectToAction("Login");
                }
                if (userByEmail != null)
                {
                    ModelState.AddModelError("Email", "User with such E-Mail already exists");
                }
                if (userByName != null)
                {
                    ModelState.AddModelError("Name", "User with such Name already exists");
                }
            }
            return View(registerModel);
        }
        /// <summary>
        /// Действие выхода из аккаунта
        /// </summary>
        /// <returns>Главная страница</returns>
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        /// <summary>
        /// Удаление песни
        /// </summary>
        /// <param name="songId">Id песни, которую надо удалить</param>
        /// <returns>Главна страница</returns>
        [Authorize]
        public ActionResult DeleteSong(int songId)
        {
            var currentUser = DataManager.Users.GetCurrentUser(this);
            if (currentUser != null)
            {
                var song = currentUser.Songs.FirstOrDefault(s => s.Id == songId);
                currentUser.Songs.Remove(song);
                DataManager.Users.SaveUser(currentUser);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}