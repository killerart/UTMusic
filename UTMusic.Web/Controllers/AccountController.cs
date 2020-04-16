using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UTMusic.BusinessLogic.DataTransfer;
using UTMusic.BusinessLogic.Interfaces;
using UTMusic.Web.Models;

namespace UTMusic.Web.Controllers
{
    public class AccountController : Controller
    {
        private IUserService UserService { get; }
        public AccountController(IUserService userService)
        {
            UserService = userService;
        }
        private UserDTO LoggedUser {
            get {
                Int32.TryParse(User.Identity.Name, out int id);
                var userDTO = UserService.GetUser(id);
                return userDTO;
            }
        }
        /// <summary>
        /// Действие страницы логина
        /// </summary>
        /// <returns>Страница регистрации</returns>
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (LoggedUser == null)
                {
                    FormsAuthentication.SignOut();
                    return RedirectToAction("Login");
                }
                return RedirectToAction("Index", "Home");
            }
            return View(new LoginModel());
        }
        /// <summary>
        /// Действие страницы регистрации
        /// </summary>
        /// <returns>Страница регистрации</returns>
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (LoggedUser == null)
                {
                    FormsAuthentication.SignOut();
                    return RedirectToAction("Register");
                }
                return RedirectToAction("Index", "Home");
            }
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
                var userDTO = new UserDTO { Email = loginModel.Email, Password = loginModel.Password };
                var authenticationResult = UserService.Authenticate(userDTO);
                if (authenticationResult.Succedeed)
                {
                    FormsAuthentication.SetAuthCookie(authenticationResult.Message, loginModel.Remember);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", authenticationResult.Message);
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
                var userDTO = new UserDTO { Email = registerModel.Email, Name = registerModel.Name, Password = registerModel.Password };
                var registerResults = UserService.Create(userDTO);
                if (registerResults.All(r => r.Succedeed))
                {
                    return RedirectToAction("Login");
                }
                foreach (var result in registerResults)
                {
                    ModelState.AddModelError(result.Property, result.Message);
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
    }
}