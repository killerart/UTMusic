using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTMusic.BusinessLogic.DataTransfer;
using UTMusic.BusinessLogic.Interfaces;

namespace UTMusic.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected IUserService UserService {
            get {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }
        protected IAuthenticationManager AuthenticationManager {
            get {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        protected UserDTO LoggedUser {
            get {
                UserDTO userDTO = null;
                if (AuthenticationManager.User.Identity.IsAuthenticated)
                {
                    var name = AuthenticationManager.User.Identity.Name;
                    userDTO = UserService.GetUser(name);
                }
                return userDTO;
            }
        }
    }
}