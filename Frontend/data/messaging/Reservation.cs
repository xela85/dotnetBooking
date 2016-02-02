using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.messaging
{
    public class Reservation
    {

    public const String QUEUE_IP = "127.0.0.1";
    public const String QUEUE_PATH = @".\private$\dotNetBooking";

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
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
    }
}
