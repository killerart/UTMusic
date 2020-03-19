using UTMusic.BusinessLogic.Core;
using UTMusic.BusinessLogic.Interfaces;
using UTMusic.Domain.Entities.General;
using UTMusic.Domain.Entities.User;

namespace UTMusic.BusinessLogic
{
    internal class SessionBL : UserApi, ISession
    {
        public ResponseMessage GetUserSession(UserSessionData userData)
        {
            return UserSession(userData);
        }
    }
}