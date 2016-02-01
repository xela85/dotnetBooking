using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace pusher
{
    using pusher.data;

    public class ReservationService
    {

        private const String QUEUE_IP = "127.0.0.1";
        private const String QUEUE_PATH = "FormatName:Direct=TCP:" + QUEUE_IP + @"\private$\dotNetBooking";

        public static void book(HotelReservation hotel, FlightReservation flight)
        {
            Reservation reservation = new Reservation();
            reservation.Flight = flight;
            reservation.Hotel = hotel;

            MessageQueue queue = new MessageQueue(QUEUE_PATH);
            queue.Send(reservation);
        }

    }
}
