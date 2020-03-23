using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTMusic.BusinessLogic;
using UTMusic.BusinessLogic.Implementations;
using UTMusic.BusinessLogic.Interfaces;
using UTMusic.Data.Entities;
using UTMusic.Web.Models;

namespace UTMusic.Web.Controllers
{
    public class HomeController : Controller
    {
        private DataManager DataManager { get; } = new DataManager();
        // GET: Home
        public ActionResult Index()
        {
            var songList = new SongListModel { Songs = DataManager.Songs.GetAllSongs() };
            return View(songList);
        }

        [HttpPost]
        public ActionResult Add(HttpPostedFileBase file)
        {
            if (file != null)
            {
                var fileName = Path.GetFileName(file.FileName);
                var fileSavePath = Server.MapPath("~/Music/" +
                  fileName);
                var songName = Path.GetFileNameWithoutExtension(file.FileName);
                var extention = Path.GetExtension(file.FileName);
                if (extention == ".mp3" && DataManager.Songs.GetSongByName(songName) == null)
                {
                    file.SaveAs(fileSavePath);
                    DataManager.Songs.SaveSong(new Song { Name = songName });
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int songId)
        {
            var song = DataManager.Songs.GetSongById(songId);
            if (song != null)
                DataManager.Songs.DeleteSong(song);
            return RedirectToAction("Index");
        }
    }
}