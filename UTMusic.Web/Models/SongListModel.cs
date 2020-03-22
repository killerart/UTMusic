using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UTMusic.Data.Entities;

namespace UTMusic.Web.Models
{
    public class SongListModel
    {
        public List<Song> Songs { get; set; }
    }
}