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
            TripService tripService = new TripService(userSessionManager);
            User nullUser = null;
            Mock.Get(userSessionManager).Setup(usm => usm.GetLoggedUser()).Returns(nullUser);

            Assert.Throws<UserNotLoggedInException>(() => tripService.GetTripsByUser(null));
        }

        [Fact]
        public void returns_empty_list_when_the_user_doesnt_have_friends()
        {
            UserSessionManager userSessionManager = Mock.Of<UserSessionManager>();
            TripService tripService = new TripService(userSessionManager);
            User userWithoutFriends = new User();
            User loggedUser = new User();
            Mock.Get(userSessionManager).Setup(usm => usm.GetLoggedUser()).Returns(loggedUser);

            List<Trip> trips = tripService.GetTripsByUser(userWithoutFriends);

            Assert.Empty(trips);
        }
    }
}
