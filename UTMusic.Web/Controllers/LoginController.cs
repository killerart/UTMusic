using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTMusic.BusinessLogic;
using UTMusic.BusinessLogic.Interfaces;
using UTMusic.Domain.Entities.User;
using UTMusic.Web.Models;

namespace UTMusic.Web.Controllers
{
    public class LoginController : Controller
    {
        public readonly ISession session; 
        public LoginController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            session = bl.GetSessionBL();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index(UserLoginData userLoginData)
        {
            UserSessionData userSessionData = new UserSessionData
            {
                Name = userLoginData.Name,
                Password = userLoginData.Password,
                SessionDate = DateTime.Now
            };
            return View();
        } 
    }
}