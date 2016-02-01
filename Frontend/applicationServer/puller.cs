using System;
using System.Diagnostics;
using System.ServiceProcess;
using data.messaging;
using libHotelReservations.Models;
using System.Messaging;

namespace applicationServer
{
    public partial class Puller : ServiceBase
    {
        private const float SLEEP_MINUTES = 0.5F;
        private IHotelReservationsApi hotels = new HotelReservationDb();
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
            MessageQueue queue = new MessageQueue(Reservation.QUEUE_PATH);
            queue.ReceiveCompleted += new ReceiveCompletedEventHandler(readAndPeek);
            queue.BeginReceive();
        }

        protected override void OnStop()
        {
        }

        public void readAndPeek(object sender, ReceiveCompletedEventArgs args)
        {
            MessageQueue queue = (MessageQueue)sender;
            try { 
            this.logger.WriteEntry("started peek on " + Reservation.QUEUE_PATH);
            
            Message msg = queue.EndReceive(args.AsyncResult);
            msg.Formatter = new XmlMessageFormatter(new Type[] { typeof(Reservation) });
            Reservation reservation = (Reservation) msg.Body;

            this.logger.WriteEntry("found hotel " + reservation.Hotel.HotelId);

            var hotelRes = new libHotelReservations.Models.HotelReservation();
            hotelRes.DepartureDate = reservation.Hotel.DepartureDate;
            hotelRes.ArrivalDate = reservation.Hotel.ArrivalDate;
            hotelRes.HotelId = reservation.Hotel.HotelId;
            hotels.book(hotelRes);
            this.logger.WriteEntry("peek done");
            } catch(Exception e)
            {
                this.logger.WriteEntry(e.Message);
            }
            queue.BeginReceive();
        }
    }
}
