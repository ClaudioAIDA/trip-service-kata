﻿using System.Collections.Generic;
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
            User loggedUser = userSessionManager.GetLoggedUser();
            bool isFriend = false;
            if (loggedUser != null)
            {
                foreach (User friend in user.GetFriends())
                {
                    if (friend.Equals(loggedUser))
                    {
                        isFriend = true;
                        break;
                    }
                }

                if (isFriend)
                {
                    tripList = tripDaoWrapper.FindTripsByUser(user);
                }

                return tripList;
            }
            else
            {
                throw new UserNotLoggedInException();
            }
        }
    }
}