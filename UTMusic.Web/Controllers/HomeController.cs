using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UTMusic.BusinessLogic.DataTransfer;
using UTMusic.BusinessLogic.Interfaces;
using UTMusic.BusinessLogic.Services;
using UTMusic.Web.Models;

namespace UTMusic.Web.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Менеджер репозиториев
        /// </summary>
        private IUserService UserService { get; }
        private IMusicService MusicService { get; }
        public HomeController(IUserService userService, IMusicService musicService)
        {
            UserService = userService;
            MusicService = musicService;
        }
        private UserDTO LoggedUser {
            get {
                Int32.TryParse(User.Identity.Name, out int id);
                var userDTO = UserService.GetUser(id);
                return userDTO;
            }
        }
        /// <summary>
        /// Действие главной страницы
        /// </summary>
        /// <returns>Главная страница</returns>
        [HttpGet]
        public ActionResult Index()
        {
            var currentUser = LoggedUser;
            if (currentUser == null && User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Index");
            }
            var model = new HomePageModel
            {
                CurrentUser = currentUser,
                UserSongs = currentUser?.Songs,
                AllSongs = MusicService.GetSongs()
            };

            return View(model);
        }
        [HttpPost]
        public ActionResult SearchSong(string searchValue)
        {
            var currentUser = LoggedUser;
            if (currentUser == null && User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Index", new { searchValue });
            }
            var model = new HomePageModel
            {
                CurrentUser = currentUser,
                UserSongs = currentUser?.Songs,
                AllSongs = MusicService.GetSongs()
            };

            if (!String.IsNullOrEmpty(searchValue))
            {
                Func<SongDTO, bool> searchFunc = (song) => song.Name.IndexOf(searchValue, StringComparison.InvariantCultureIgnoreCase) != -1;
                model.UserSongs = model.UserSongs?.Where(searchFunc);
                model.AllSongs = model.AllSongs?.Where(searchFunc);
            }
            return PartialView("SongList", model);
        }
        /// <summary>
        /// Загрузка песни на сайт
        /// </summary>
        /// <param name="file">Файл с песней в формате .mp3</param>
        /// <returns>Главная страница</returns>
        [HttpPost]
        public ActionResult UploadSong(HttpPostedFileBase file)
        {
            var currentUser = LoggedUser;
            if (currentUser == null && User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("UploadSong", new { file });
            }
            if (file != null)
            {
                var extention = Path.GetExtension(file.FileName);
                if (extention == ".mp3")
                {
                    var songName = Path.GetFileNameWithoutExtension(file.FileName);
                    var fileName = songName;
                    if (MusicService.FileExists(fileName))
                    {
                        fileName += "1";
                    }
                    var fileSavePath = Server.MapPath("~/Music/" +
                      fileName + extention);
                    file.SaveAs(fileSavePath);

                    var songDTO = new SongDTO { Name = songName, FileName = fileName };
                    if (currentUser != null)
                    {
                        UserService.AddNewSong(ref currentUser, songDTO);
                    }
                    else
                    {
                        MusicService.AddSong(songDTO);
                    }
                }
            }
            var model = new HomePageModel
            {
                CurrentUser = currentUser,
                UserSongs = currentUser?.Songs,
                AllSongs = MusicService.GetSongs()
            };
            return PartialView("SongList", model);
        }
        [Authorize]
        public ActionResult AddSong(int songId)
        {
            var currentUser = LoggedUser;
            if (currentUser == null && User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("AddSong", new { songId });
            }
            UserService.AddExistingSong(ref currentUser, songId);
            var model = new HomePageModel
            {
                CurrentUser = currentUser,
                UserSongs = currentUser?.Songs,
                AllSongs = MusicService.GetSongs()
            };
            return PartialView("SongList", model);
        }
        /// <summary>
        /// Удаление песни
        /// </summary>
        /// <param name="songId">Id песни, которую надо удалить</param>
        /// <returns>Главна страница</returns>
        [Authorize]
        public ActionResult DeleteSong(int songId)
        {
            var currentUser = LoggedUser;
            if (currentUser == null && User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("DeleteSong", new { songId });
            }
            UserService.DeleteSong(ref currentUser, songId);
            var model = new HomePageModel
            {
                CurrentUser = currentUser,
                UserSongs = currentUser?.Songs,
                AllSongs = MusicService.GetSongs()
            };
            return PartialView("SongList", model);
        }
    }
}