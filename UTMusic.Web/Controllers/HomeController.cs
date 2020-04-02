using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UTMusic.BusinessLogic;
using UTMusic.BusinessLogic.Implementations;
using UTMusic.BusinessLogic.Interfaces;
using UTMusic.Data.Entities;
using UTMusic.Web.Models;

namespace UTMusic.Web.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Менеджер репозиториев
        /// </summary>
        private DataManager DataManager { get; } = new DataManager();
        private User LoggedUser {
            get {
                var user = DataManager.Users.GetCurrentUser(this);
                return user;
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
            var model = new SongListModel
            {
                CurrentUser = currentUser,
                UserSongs = currentUser?.GetOrderedSongs(),
                AllSongs = DataManager.Songs.GetAllSongs()?.Reverse()
            };

            return View(model);
        }
        [HttpPost]
        public ActionResult Index(string searchValue)
        {
            var currentUser = LoggedUser;
            if (currentUser == null && User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Index", new { searchValue });
            }
            var model = new SongListModel
            {
                CurrentUser = currentUser,
                UserSongs = currentUser?.GetOrderedSongs(),
                AllSongs = DataManager.Songs.GetAllSongs()?.Reverse()
            };

            if (!String.IsNullOrEmpty(searchValue))
            {
                model.UserSongs = model.UserSongs?.Where(song => song.Name.IndexOf(searchValue, StringComparison.InvariantCultureIgnoreCase) != -1);
                model.AllSongs = model.AllSongs?.Where(song => song.Name.IndexOf(searchValue, StringComparison.InvariantCultureIgnoreCase) != -1);
            }
            return View(model);
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
                    if (DataManager.Songs.GetSongByFileName(fileName) != null)
                    {
                        fileName += "1";
                    }
                    var fileSavePath = Server.MapPath("~/Music/" +
                      fileName + extention);
                    var song = new Song { Name = songName, FileName = fileName };
                    if (currentUser != null)
                    {
                        currentUser.Songs.Add(song);
                        DataManager.Users.SaveUser(currentUser);
                        currentUser.OrderOfSongs.Add(new IdNumber { SongId = DataManager.Songs.GetSongByFileName(fileName).Id });
                        DataManager.Users.SaveUser(currentUser);
                    }
                    else
                    {
                        DataManager.Songs.SaveSong(song);
                    }
                    file.SaveAs(fileSavePath);
                }
            }
            return RedirectToAction("Index");
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
            var song = DataManager.Songs.GetSongById(songId);
            if (currentUser != null && song != null)
            {
                currentUser.Songs.Add(song);
                currentUser.OrderOfSongs.Add(new IdNumber { SongId = songId });
                DataManager.Users.SaveUser(currentUser);
            }
            return RedirectToAction("Index");
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
            if (currentUser != null)
            {
                var song = currentUser.Songs.FirstOrDefault(s => s.Id == songId);
                currentUser.Songs.Remove(song);
                IdNumber idNumber = currentUser.OrderOfSongs.FirstOrDefault(i => i.SongId == songId);
                currentUser.OrderOfSongs.Remove(idNumber);
                DataManager.Users.SaveUser(currentUser);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}