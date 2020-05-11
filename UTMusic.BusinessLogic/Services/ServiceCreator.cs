﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTMusic.BusinessLogic.Services;
using UTMusic.DataAccess.Repositories;

namespace UTMusic.BusinessLogic.Interfaces
{
    public class ServiceCreator : IServiceCreator
    {
        private string Connection { get; }
        public ServiceCreator(string connection)
        {
            Connection = connection;
        }
        public IUserApi CreateUserService()
        {
            return new UserService(new UnitOfWork(Connection));
        }
        public IAdminApi CreateAdminService()
        {
            return new AdminService(new UnitOfWork(Connection));
        }
    }
}
