using TripServiceKata.Exception;
using Xunit;

namespace TripServiceKata.Tests
{
    public class TripServiceShould
    {

        [Fact]
        public void throw_UserNotLoggedInException_when_the_user_is_not_logged_in()
        {
            TripService tripService = new TripService();

            Assert.Throws<UserNotLoggedInException>(() => tripService.GetTripsByUser(null));
        }
    }
}
