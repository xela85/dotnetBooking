using System.Collections.Generic;
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
