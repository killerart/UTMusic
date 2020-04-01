using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UTMusic.Web.Models
{
    public class RegisterModel : HeaderModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}