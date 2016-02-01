using data.messaging;
using pusher;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HotelReservation hotelRes = new HotelReservation();
            hotelRes.DepartureDate = DateTime.Now;
            hotelRes.ArrivalDate = DateTime.Now;
            hotelRes.HotelId = 1;

            FlightReservation flightRes = new FlightReservation();
            flightRes.DepartureDate = DateTime.Now;
            flightRes.ArrivalDate = DateTime.Now;
            flightRes.FlightId = 1;
            ReservationService.book(hotelRes, flightRes);
        }
    }
}
