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
        /// <summary>
        /// Действие главной страницы
        /// </summary>
        /// <returns>Главная страница</returns>
        [HttpGet]
        public ActionResult Index()
        {
            var model = new SongListModel();
            model.UserSongs = DataManager.Users.GetCurrentUser(this)?.Songs?.AsEnumerable();
            model.AllSongs = DataManager.Songs.GetAllSongs();
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(string searchValue)
        {
            var model = new SongListModel();
            model.UserSongs = DataManager.Users.GetCurrentUser(this)?.Songs?.AsEnumerable();
            model.AllSongs = DataManager.Songs.GetAllSongs();

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
                    var currentUser = DataManager.Users.GetCurrentUser(this);
                    if (currentUser != null)
                    {
                        currentUser.Songs.Add(song);
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
            var user = DataManager.Users.GetCurrentUser(this);
            var song = DataManager.Songs.GetSongById(songId);
            if (user != null && song != null)
            {
                user.Songs.Add(song);
                DataManager.Users.SaveUser(user);
            }
            return RedirectToAction("Index");
        }
    }
}