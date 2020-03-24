﻿using System;
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
        private DataManager DataManager { get; } = new DataManager();
        // GET: Home
        public ActionResult Index()
        {
            var user = User.Identity.IsAuthenticated ? DataManager.Users.GetCurrentUser(this) : null;
            ViewBag.Songs = DataManager.Songs.GetAllSongs();
            return View(user);
        }

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
                    var song = DataManager.Songs.SaveSong(new Song { Name = songName, FileName = fileName });
                    file.SaveAs(fileSavePath);
                    if (User.Identity.IsAuthenticated)
                    {
                        var currentUser = DataManager.Users.GetCurrentUser(this);
                        currentUser.Songs.Add(song);
                        DataManager.Users.SaveUser(currentUser);
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}