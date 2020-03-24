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
        private DataManager DataManager { get; } = new DataManager();
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
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
            loginModel.Password = "";
            return View(loginModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                User userByEmail = DataManager.Users.GetUserByEmail(registerModel.Email);
                User userByName = DataManager.Users.GetUserByName(registerModel.Email);
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
                    ModelState.AddModelError("", "User with such E-Mail already exists");
                    registerModel.Email = "";
                }
                if (userByName != null)
                {
                    ModelState.AddModelError("", "User with such Name already exists");
                    registerModel.Name = "";
                }
            }
            return View(registerModel);
        }
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public ActionResult DeleteSong(int songId)
        {
            var song = DataManager.Songs.GetSongById(songId);
            if (song != null)
            {
                /*var currentUser = DataManager.Users.GetCurrentUser(this);
                currentUser?.Songs.Remove(song);
                DataManager.Users.SaveUser(currentUser);*/
                DataManager.Songs.DeleteSong(song);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}