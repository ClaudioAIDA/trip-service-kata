using System.Collections.Generic;
using TripServiceKata.Entity;

namespace TripServiceKata.Service
{
    public class TripDAOWrapper
    {
        public TripDAOWrapper()
        {
        }

        public virtual List<Trip> FindTripsByUser(User user)
        {
            return TripDAO.FindTripsByUser(user);
        }
    }
}