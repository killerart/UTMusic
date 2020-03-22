using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTMusic.BusinessLogic.Implementations;
using UTMusic.BusinessLogic.Interfaces;
using UTMusic.Data.Entities;
using UTMusic.Web.Models;

namespace UTMusic.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISongsRepository _songsRepository = new EFSongsRepository(new SongContext());
        // GET: Home
        public ActionResult Index()
        {
            var songList = new SongListModel { Songs = _songsRepository.GetAllSongs() };
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
                if (extention == ".mp3" && _songsRepository.GetSongByName(songName) == null)
                {
                    file.SaveAs(fileSavePath);
                    _songsRepository.SaveSong(new Song { Name = songName, Duration = DateTime.Parse("00:00:00") });
                }
            }
            return RedirectToAction("Index");
        }
    }
}