using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UTMusic.DataAccess.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Song> Songs { get; set; }
        public virtual ICollection<IdNumber> OrderOfSongs { get; set; }
        public User()
        {
            Songs = new List<Song>();
            OrderOfSongs = new List<IdNumber>();
        }
        public List<Song> GetOrderedSongs()
        {
            var orderedSongs = new List<Song>();
            for (int i = OrderOfSongs.Count - 1; i >= 0; --i)
            {
                var song = Songs.FirstOrDefault(s => s.Id == OrderOfSongs.ElementAt(i).SongId);
                if (song != null)
                    orderedSongs.Add(song);
            }
            return orderedSongs;
        }
    }
}