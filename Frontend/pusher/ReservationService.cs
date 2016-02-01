using System;
using System.Messaging;

namespace pusher
{
    using data.messaging;

    public class ReservationService
    {

        public static void book(HotelReservation hotel, FlightReservation flight)
        {
            Reservation reservation = new Reservation();
            reservation.Flight = flight;
            reservation.Hotel = hotel;

            MessageQueue queue = new MessageQueue(Reservation.QUEUE_PATH);
            queue.Send(reservation);
        }

    }
}
