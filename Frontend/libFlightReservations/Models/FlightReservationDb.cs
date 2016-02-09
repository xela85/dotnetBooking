using System.Collections.Generic;
using System.Linq;
using data.messaging;

namespace libFlightReservations.Models
{
    public class FlightReservationDb : IFlightReservationsApi
    {
        FlightReservationContext db = new FlightReservationContext();

        public void book(FlightReservation flightRes)
        {
            db.FlightReservations.Add(flightRes);
            db.SaveChanges();
        }

        public IEnumerable<FlightReservation> fromFlight(int flightId)
        {
            return db.FlightReservations.Where(f => f.FlightId == flightId);
        }

        public IEnumerable<FlightReservation> getFlights()
        {
            return db.FlightReservations;
        }
    }
}
