using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using pusher;
using data.messaging;
using libHotels.Models;
using libAirports.Models;
namespace frontend.Controllers
{
    public class BookController : ApiController
    {
        [HttpPost]
        [Route("api/book/")]
        public Response Book([FromBody]TravelReservation travelReservation)
        {
            try
            {
                HotelReservation HotelReservation = new HotelReservation();
                HotelReservation.HotelId = travelReservation.hotel.Id;
                HotelReservation.ArrivalDate = travelReservation.hotelArrivalDate;
                HotelReservation.DepartureDate = travelReservation.hotelDepartureDate;

                FlightReservation FlightReservation = new FlightReservation();
                FlightReservation.ArrivalDate = travelReservation.hotelArrivalDate;
                FlightReservation.DepartureDate = travelReservation.hotelArrivalDate;
                FlightReservation.FlightId = travelReservation.flight.Id;
                ReservationService.book(HotelReservation,FlightReservation);
                return new Response(false,"Your booking request has been registered.");

            }
            catch
            {
                return new Response(true,"Error : impossible to register your booking.");
            }
        }
        
    }
    public class User
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Mail { get; set; }
        public String BankCard { get; set; }
    }
    public class Response
    {
        public bool Error { get; set; }
        public String Message { get; set; }
        public Response(bool error,String message)
        {
            Error = error;
            Message = message;
        }
    }
    public class TravelReservation
    {
        public User user { get; set; }
        public Hotel hotel { get; set; }
        public Flight flight { get; set; }
        public DateTime hotelArrivalDate { get; set; }
        public DateTime hotelDepartureDate { get; set; }
    }
}
