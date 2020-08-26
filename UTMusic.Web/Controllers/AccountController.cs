using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UTMusic.BusinessLogic.DataTransfer;
using UTMusic.BusinessLogic.Interfaces;
using UTMusic.Web.Models;
using Microsoft.AspNet.Identity.Owin;

namespace UTMusic.Web.Controllers
{
    public class AccountController : BaseController
    {
        /// <summary>
        /// Действие страницы логина
        /// </summary>
        /// <returns>Страница регистрации</returns>
        public ActionResult Login()
        {
            if (AuthenticationManager.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View(new LoginModel());
        }
        /// <summary>
        /// Действие страницы регистрации
        /// </summary>
        /// <returns>Страница регистрации</returns>
        public ActionResult Register()
        {
            if (AuthenticationManager.User.Identity.IsAuthenticated)
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
        public async Task<ActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var userDTO = new UserDTO { UserName = loginModel.Username, Password = loginModel.Password };
                var claim = await UserService.Authenticate(userDTO);
                if (claim != null)
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = loginModel.Remember
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Incorrect login data or you didn't confirm your Email");
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
        public async Task<ActionResult> Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var userDTO = new UserDTO
                {
                    Email = registerModel.Email,
                    UserName = registerModel.UserName,
                    Password = registerModel.Password,
                    Role = "user"
                };
                var registerResults = await UserService.Create(userDTO, Request, Url);
                if (registerResults.All(r => r.Succeeded))
                    return View("DisplayEmail");
                else
                    foreach (var result in registerResults)
                        ModelState.AddModelError(result.Property, result.Message);
            }
            return View(registerModel);
        }

        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            var result = await UserService.ConfirmEmail(userId, code);
            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }
            return View("Error");
        }

        /// <summary>
        /// Действие выхода из аккаунта
        /// </summary>
        /// <returns>Главная страница</returns>
        [Authorize]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}