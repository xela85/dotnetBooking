using System;
using System.Diagnostics;
using System.ServiceProcess;
using data.messaging;
using libHotelReservations.Models;
using System.Messaging;
using libFlightReservations.Models;
using System.Transactions;

namespace applicationServer
{
    public partial class Puller : ServiceBase
    {
        private const float SLEEP_MINUTES = 0.5F;
        private IHotelReservationsApi hotels = new HotelReservationDb();
        private IFlightReservationsApi flights = new FlightReservationDb();
        private EventLog logger;

        public Puller()
        {
            InitializeComponent();
            this.ServiceName = "Booking EMN";
            this.logger = new EventLog();
            this.logger.Source = this.ServiceName;
            this.logger.Log = "Application";

        }


        protected override void OnStart(string[] args)
        {
            flights.getFlights();
            hotels.GetHotelReservations();
            MessageQueue queue = new MessageQueue(Reservation.QUEUE_PATH);
            queue.PeekCompleted += new PeekCompletedEventHandler(readAndPeek);
            queue.BeginPeek();
        }

        protected override void OnStop()
        {
        }

        public void readAndPeek(object sender, PeekCompletedEventArgs args)
        {
            MessageQueue queue = (MessageQueue)sender;
            try
            {
                this.logger.WriteEntry("started peek on " + Reservation.QUEUE_PATH);

                Message msg = queue.EndReceive(args.AsyncResult);
                msg.Formatter = new XmlMessageFormatter(new Type[] { typeof(Reservation) });
                Reservation reservation = (Reservation)msg.Body;
                this.logger.WriteEntry("found hotel " + reservation.Hotel.HotelId);
                saveReservation(reservation);
                queue.Receive();
                this.logger.WriteEntry("peek done");
            }
            catch (Exception e)
            {
                this.logger.WriteEntry(e.Message);
            }
            queue.BeginPeek();
        }

        private void saveReservation(Reservation reservation)
        {
            using (var txScope = new TransactionScope())
            {
                var hotelRes = new libHotelReservations.Models.HotelReservation();
                hotelRes.DepartureDate = reservation.Hotel.DepartureDate;
                hotelRes.ArrivalDate = reservation.Hotel.ArrivalDate;
                hotelRes.HotelId = reservation.Hotel.HotelId;
                hotels.book(hotelRes);

                var flightRes = new libFlightReservations.Models.FlightReservation();
                flightRes.DepartureDate = reservation.Flight.DepartureDate;
                flightRes.ArrivalDate = reservation.Flight.ArrivalDate;
                flightRes.FlightId = reservation.Flight.FlightId;
                flights.book(flightRes);

                // Finally, commit the MSDTC transaction
                txScope.Complete();
            }
        }

    }
}
