using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UTMusic.Data.Enums;

namespace UTMusic.Data.Entities
{
    public class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public virtual ICollection<Genres> SongGenres { get; set; }
        public Song()
        {
            SongGenres = new List<Genres>();
        }
    }
}