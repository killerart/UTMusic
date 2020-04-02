using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UTMusic.Data.Entities;

namespace UTMusic.Web.Models
{
    public class HomePageModel : HeaderModel
    {
        public string SearchValue { get; set; }
        public IEnumerable<Song> UserSongs { get; set; }
        public IEnumerable<Song> AllSongs { get; set; }
    }
}