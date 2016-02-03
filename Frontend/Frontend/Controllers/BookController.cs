using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using pusher;
using data.messaging;
namespace frontend.Controllers
{
    public class BookController : ApiController
    {
        [HttpPost]
        [Route("api/book/")]
        public String Book(HotelReservation hotelReservation, FlightReservation flightReservation, User )
        {
            return "La réservation ;
        }
        [HttpGet]
        [Route("api/airports/")]
        public IEnumerable<Airport> All()
        {
            return airports.GetAirports();
        }
    }
    public class Error
    {
        public String Message {get;set;}
        public Exception Exception {get;set;}
    }
}
