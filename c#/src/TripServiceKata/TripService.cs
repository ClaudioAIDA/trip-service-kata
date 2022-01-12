using System.Collections.Generic;
using System.Linq;
using TripServiceKata.Entity;
using TripServiceKata.Exception;
using TripServiceKata.Service;

namespace TripServiceKata
{
    public class TripService
    {
        private readonly UserSessionManager userSessionManager;
        private readonly TripDAOWrapper tripDaoWrapper;

        public TripService(UserSessionManager userSessionManager, TripDAOWrapper daoWrapper)
        {
            this.userSessionManager = userSessionManager;
            tripDaoWrapper = daoWrapper;
        }

        public List<Trip> GetTripsByUser(User user)
        {
            List<Trip> tripList = new List<Trip>();
            var loggedUser = GetLoggedUser();

            bool isFriend = user.GetFriends().Contains(loggedUser);

            if (isFriend)
            {
                tripList = tripDaoWrapper.FindTripsByUser(user);
            }

            return tripList;

        }

        private User GetLoggedUser()
        {
            User loggedUser = userSessionManager.GetLoggedUser();

            if (loggedUser == null) throw new UserNotLoggedInException();
            return loggedUser;
        }
    }
}