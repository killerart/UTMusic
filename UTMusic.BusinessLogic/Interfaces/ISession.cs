using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTMusic.Domain.Entities.General;
using UTMusic.Domain.Entities.User;

namespace UTMusic.BusinessLogic.Interfaces
{
    public interface ISession
    {
        ResponseMessage GetUserSession(UserSessionData userData);
    }
}
