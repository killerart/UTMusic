using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UTMusic.Domain.Enums;

namespace UTMusic.Data.Entities
{
    public class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Genres> SongGenres { get; set; }
    }
}