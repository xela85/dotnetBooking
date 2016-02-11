using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
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
        public int Id { get; set; }
        public int HotelId { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
    }

    public class FlightReservation
    {
        public int Id { get; set; }
        public int FlightId { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
    }
    public class Airport
    {
        [Key]
        public int Id { get; set; }
        [Index("CodeIndex", IsUnique = true), StringLength(3), Required]
        public String Code { get; set; }
        public String Name { get; set; }
        public String City { get; set; }
        public String Country { get; set; }
        public String Timezone { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
    public class Flight
    {
        public int Id { get; set; }
        [Required]
        public int DepartureAirportId { get; set; }
        [Required]
        public int ArrivalAirportId { get; set; }

        public Airport DepartureAirport { get; set; }
        public Airport ArrivalAirport { get; set; }
    }
    public class User
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Mail { get; set; }
        public String BankCard { get; set; }
    }
}
