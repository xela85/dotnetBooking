using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using data.messaging;
namespace libHotelReservations.Models
{
    public interface IHotelReservationsApi
    {
        IEnumerable<HotelReservation> GetHotelReservations();
        IEnumerable<HotelReservation> FromHotel(int hotelId);
        void book(HotelReservation reservation);
    }
}
