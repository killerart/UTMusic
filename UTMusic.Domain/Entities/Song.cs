using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UTMusic.DataAccess.Entities
{
    public class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public Song()
        {
            Users = new HashSet<User>();
        }
    }
}