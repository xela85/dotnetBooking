using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pusher.data
{
    public class Reservation
    {
        public HotelReservation Hotel { get; set; }
        public FlightReservation Flight { get; set; }
    }

    public class HotelReservation
    {
        public int HotelId { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
    }

    public class FlightReservation
    {
        public int FlightId { get; set; }
        public int HotelId { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
    }
}
