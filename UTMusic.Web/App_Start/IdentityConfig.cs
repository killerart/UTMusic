using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UTMusic.BusinessLogic.Interfaces;
using UTMusic.BusinessLogic.Services;

[assembly: OwinStartup(typeof(UTMusic.Web.IdentityConfig))]

namespace UTMusic.Web
{
    public class IdentityConfig
    {
        IServiceCreator serviceCreator = new ServiceCreator(); 
        private IUserService CreateUserService()
        {
            return serviceCreator.CreateUserService("MusicContext");
        }
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(CreateUserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
        }
    }
}