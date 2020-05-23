using System.Web;
using System.Web.Mvc;
using UTMusic.BusinessLogic.DataTransfer;
using UTMusic.BusinessLogic.Interfaces;
using UTMusic.Web.Attributes;
using UTMusic.Web.Models;

namespace UTMusic.Web.Controllers
{
    public class HomeController : BaseController
    {
        private IMusicApi MusicService { get; }
        public HomeController(IMusicApi musicService)
        {
            MusicService = musicService;
        }
        /// <summary>
        /// Действие главной страницы
        /// </summary>
        /// <returns>Главная страница</returns>
        [HttpGet]
        public ActionResult Index()
        {
            var currentUser = LoggedUser;
            var model = new SongListModel
            {
                CurrentUser = currentUser,
                UserSongs = currentUser?.Songs,
                AllSongs = MusicService.GetSongs()
            };
            TempData["searchValue"] = string.Empty;
            return View(model);
        }
        [HttpPost]
        public ActionResult SearchSong(string searchValue)
        {
            var currentUser = LoggedUser;
            var model = new SongListModel
            {
                CurrentUser = currentUser,
                UserSongs = MusicService.SearchSongs(searchValue, currentUser?.Songs),
                AllSongs = MusicService.SearchSongs(searchValue)
            };
            TempData["searchValue"] = searchValue;
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
            SongDTO songDTO = null;
            var result = MusicService.SaveSongToDisk(file, Server.MapPath("~/Music"), out songDTO);
            if (result.Succeeded)
            {
                if (currentUser != null)
                {
                    UserService.AddNewSong(currentUser, songDTO);
                }
                else
                {
                    MusicService.AddSong(songDTO);
                }
            }
            return SearchSong(TempData["searchValue"] as string);
        }
        [Authorize]
        public ActionResult AddSong(int songId)
        {
            var currentUser = LoggedUser;
            UserService.AddExistingSong(currentUser, songId);
            return SearchSong(TempData["searchValue"] as string);
        }
        /// <summary>
        /// Удаление песни
        /// </summary>
        /// <param name="songId">Id песни, которую надо удалить</param>
        /// <returns>Главна страница</returns>
        [Authorize]
        public ActionResult RemoveSong(int songId)
        {
            var currentUser = LoggedUser;
            UserService.RemoveSong(currentUser, songId);
            return SearchSong(TempData["searchValue"] as string);
        }
        [MyAuthorize(Roles = "admin")]
        public ActionResult DeleteSong(int songId)
        {
            AdminService.RemoveSong(songId, Server.MapPath("~/Music"));
            return SearchSong(TempData["searchValue"] as string);
        }
    }
}