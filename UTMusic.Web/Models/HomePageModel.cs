using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UTMusic.BusinessLogic.DataTransfer;

namespace UTMusic.Web.Models
{
    public class HomePageModel : HeaderModel
    {
        public IEnumerable<SongDTO> UserSongs { get; set; }
        public IEnumerable<SongDTO> AllSongs { get; set; }
    }
}