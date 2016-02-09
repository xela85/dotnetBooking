using data.messaging;
using System.Collections.Generic;
using System.Linq;

namespace libHotelReservations.Models
{
    public class HotelReservationDb : IHotelReservationsApi
    {
        private HotelReservationContext db = new HotelReservationContext();

        void IHotelReservationsApi.book(HotelReservation reservation)
        {
                db.HotelReservations.Add(reservation);
                db.SaveChanges();
            
        }

        IEnumerable<HotelReservation> IHotelReservationsApi.FromHotel(int hotelId)
        {
            return db.HotelReservations.Where(h => h.HotelId == hotelId);
        }

        IEnumerable<HotelReservation> IHotelReservationsApi.GetHotelReservations()
        {
            return db.HotelReservations;
        }
    }
}
