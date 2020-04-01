using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UTMusic.Data.Entities
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
            var orderOfSongs = OrderOfSongs.ToList();
            for (int i = orderOfSongs.Count - 1; i >= 0; i--)
            {
                var song = Songs.FirstOrDefault(s => s.Id == orderOfSongs[i].SongId);
                if (song != null)
                    orderedSongs.Add(song);
            }
            return orderedSongs;
        }
    }
}