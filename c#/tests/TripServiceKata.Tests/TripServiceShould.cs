using System.Collections.Generic;
using Moq;
using TripServiceKata.Entity;
using TripServiceKata.Exception;
using TripServiceKata.Service;
using Xunit;

namespace TripServiceKata.Tests
{
    public class TripServiceShould
    {

        [Fact]
        public void throw_UserNotLoggedInException_when_the_user_is_not_logged_in()
        {
            UserSessionManager userSessionManager = Mock.Of<UserSessionManager>();
            TripService tripService = new TripService(userSessionManager, new TripDAOWrapper());
            User nullUser = null;
            Mock.Get(userSessionManager).Setup(usm => usm.GetLoggedUser()).Returns(nullUser);

            Assert.Throws<UserNotLoggedInException>(() => tripService.GetTripsByUser(null));
        }

        [Fact]
        public void returns_empty_list_when_the_user_doesnt_have_friends()
        {
            UserSessionManager userSessionManager = Mock.Of<UserSessionManager>();
            TripService tripService = new TripService(userSessionManager, new TripDAOWrapper());
            User userWithoutFriends = new User();
            User loggedUser = new User();
            Mock.Get(userSessionManager).Setup(usm => usm.GetLoggedUser()).Returns(loggedUser);

            List<Trip> trips = tripService.GetTripsByUser(userWithoutFriends);

            Assert.Empty(trips);
        }

        [Fact]
        public void return_empty_list_when_the_logged_user_and_the_user_are_not_friends()
        {
            UserSessionManager userSessionManager = Mock.Of<UserSessionManager>();
            TripService tripService = new TripService(userSessionManager, new TripDAOWrapper());
            User userWithFriends = new User();
            userWithFriends.AddFriend(new User());
            User loggedUser = new User();
            Mock.Get(userSessionManager).Setup(usm => usm.GetLoggedUser()).Returns(loggedUser);

            List<Trip> trips = tripService.GetTripsByUser(userWithFriends);

            Assert.Empty(trips);
        }

        [Fact]
        public void return_a_list_when_the_logged_user_and_the_user_are_friends()
        {
            UserSessionManager userSessionManager = Mock.Of<UserSessionManager>();
            TripDAOWrapper tripDaoWrapper = Mock.Of<TripDAOWrapper>();
            TripService tripService = new TripService(userSessionManager, tripDaoWrapper);
            User loggedUser = new User();
            User userWithFriends = new User();
            userWithFriends.AddFriend(loggedUser);
            
            Mock.Get(userSessionManager).Setup(usm => usm.GetLoggedUser()).Returns(loggedUser);
            Mock.Get(tripDaoWrapper).Setup(tdw => tdw.FindTripsByUser(userWithFriends))
                .Returns(new List<Trip>() {new Trip()});

            List<Trip> trips = tripService.GetTripsByUser(userWithFriends);

            Assert.NotEmpty(trips);
        }
    }
}
