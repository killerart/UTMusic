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

[assembly: OwinStartup(typeof(UTMusic.Web.OwinConfig))]
namespace UTMusic.Web
{
    public class OwinConfig
    {
        private readonly IServiceCreator _serviceCreator = new ServiceCreator("MusicContext");
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(_serviceCreator.CreateUserService);
            app.CreatePerOwinContext(_serviceCreator.CreateAdminService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
        }
    }
}