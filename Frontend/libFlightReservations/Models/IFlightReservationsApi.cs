using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libFlightReservations.Models
{
    public interface IFlightReservationsApi
    {
        IEnumerable<FlightReservation> getFlights();
        IEnumerable<FlightReservation> fromFlight(int flightId);
        void book(FlightReservation flightRes);
    }
}
