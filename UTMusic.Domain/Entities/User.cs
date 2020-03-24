﻿using System;
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
        public User()
        {
            Songs = new List<Song>();
        }
    }
}