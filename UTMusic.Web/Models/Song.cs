using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UTMusic.Domain.Enums;

namespace UTMusic.Web.Models
{
    public struct Time
    {
        public int Minutes { get; set; }
        public int Seconds { get; set; }
    }
    public class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Filename { get; set; }
        public Time Duration { get; set; }
        public List<Genres> SongGenres { get; set; }
    }
}