using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UTMusic.BusinessLogic.Interfaces;
using UTMusic.BusinessLogic.Services;

namespace UTMusic.Web.Util
{
    public class DependencyModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMusicService>().To<MusicService>();
            Bind<IUserService>().To<UserService>();
        }
    }
}