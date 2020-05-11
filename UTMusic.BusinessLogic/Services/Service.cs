using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTMusic.DataAccess.Interfaces;

namespace UTMusic.BusinessLogic.Services
{
    public abstract class Service : IDisposable
    {
        protected IUnitOfWork Database { get; set; }
        public Service(IUnitOfWork database) => Database = database;
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
