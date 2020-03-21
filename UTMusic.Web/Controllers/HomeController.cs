using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTMusic.Web.Models;

namespace UTMusic.Web.Controllers
{
    public class HomeController : Controller
    {
        SongContext db = new SongContext();
        // GET: Home
        public ActionResult Index()
        {
            IEnumerable<Song> songs = db.Songs;
            ViewBag.Songs = songs;
            return View();
        }
    }
}