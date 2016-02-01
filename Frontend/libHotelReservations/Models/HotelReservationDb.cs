using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libHotelReservations.Models
{
    public class HotelReservationDb : IHotelReservationsApi
    {
        private HotelReservationContext db = new HotelReservationContext();

        bool IHotelReservationsApi.book(HotelReservation reservation)
        {
            try
            {
                db.HotelReservations.Add(reservation);
                db.SaveChanges();
                return true;
            } catch
            {
                return false;
            }
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
