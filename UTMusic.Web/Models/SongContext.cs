﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UTMusic.Web.Models
{
    public class SongContext : DbContext
    {
        public DbSet<Song> Songs { get; set; }
    }
}