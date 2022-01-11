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
            User nullUser = null;
            Mock.Get(userSessionManager).Setup(usm => usm.GetLoggedUser()).Returns(nullUser);

            TripService tripService = new TripService(userSessionManager);

            Assert.Throws<UserNotLoggedInException>(() => tripService.GetTripsByUser(null));
        }
    }
}
