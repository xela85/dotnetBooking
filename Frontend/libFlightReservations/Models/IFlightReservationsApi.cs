using System.Collections.Generic;
using data.messaging;

namespace libFlightReservations.Models
{
    public interface IFlightReservationsApi
    {
        IEnumerable<FlightReservation> getFlights();
        IEnumerable<FlightReservation> fromFlight(int flightId);
        void book(FlightReservation flightRes);
    }
}
