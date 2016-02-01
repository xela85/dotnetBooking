using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libHotels.Models
{
    public class HotelDb : IHotels
    {

        HotelContext db = new HotelContext();

        public IEnumerable<Hotel> FromCity(string city)
        {
            return db.Hotels.Where(h => h.City.ToLower().Contains(city));
        }

        public IEnumerable<Hotel> GetHotels()
        {
            return db.Hotels;
        }

        public bool AddHotels(IEnumerable<Hotel> hotels)
        {
            try
            {
                IEnumerable<Hotel> toInsert = hotels.Where(a => db.Hotels.FirstOrDefault(x => x.City == a.City) == null);
                db.Hotels.AddRange(toInsert);
                db.SaveChanges();
                return true;
            } catch
            {
                return false;
            }
        }

    }
}
