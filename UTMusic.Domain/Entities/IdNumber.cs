﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTMusic.DataAccess.Entities
{
    public class IdNumber
    {
        public int Id { get; set; }
        public int SongId { get; set; }
    }
}
