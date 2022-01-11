using TripServiceKata.Entity;

namespace TripServiceKata.Service
{
    public class UserSessionManager
    {
        public virtual User GetLoggedUser()
        {
            return UserSession.GetInstance().GetLoggedUser();
        }
    }
}