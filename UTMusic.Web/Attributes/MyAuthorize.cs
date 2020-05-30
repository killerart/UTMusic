using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UTMusic.BusinessLogic.Interfaces;
using Microsoft.AspNet.Identity.Owin;

namespace UTMusic.Web.Attributes
{
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        private string[] allowedRoles = new string[] { };
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!string.IsNullOrEmpty(base.Roles))
            {
                allowedRoles = base.Roles.Split(new char[] { ',' });
                for (int i = 0; i < allowedRoles.Length; i++)
                    allowedRoles[i] = allowedRoles[i].Trim();
            }
            return httpContext.Request.IsAuthenticated && Role(httpContext);
        }
        private bool Role(HttpContextBase httpContext)
        {
            var authenticationManager = httpContext.GetOwinContext().Authentication;
            if (authenticationManager.User.Identity.IsAuthenticated)
            {
                if (allowedRoles.Length > 0)
                {
                    var name = authenticationManager.User.Identity.Name;
                    var userDTO = httpContext.GetOwinContext().GetUserManager<IUserApi>().GetUser(name);
                    if (userDTO != null)
                        for (int i = 0; i < allowedRoles.Length; i++)
                            if (userDTO.Role == allowedRoles[i])
                                return true;
                    return false;
                }
                return true;
            }
            return false;
        }
    }
}