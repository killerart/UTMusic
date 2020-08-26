using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTMusic.DataAccess.Entities
{
    public class IdNumber
    {
        public int Id { get; set; }
        public virtual Song Song { get; set; }
        public virtual UserProfile User { get; set; }
    }
}
