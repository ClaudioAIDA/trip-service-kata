using TripServiceKata.Entity;

namespace TripServiceKata.Service
{
    public class UserSessionManager
    {
        public User GetLoggedUser()
        {
            return UserSession.GetInstance().GetLoggedUser();
        }
    }
}