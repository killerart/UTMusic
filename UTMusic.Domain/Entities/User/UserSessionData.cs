using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTMusic.Domain.Entities.User
{
    public class UserSessionData
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime SessionDate { get; set; }
    }
}
