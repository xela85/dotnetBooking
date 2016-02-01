using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libHotels.Models
{
    public interface IHotels
    {

        IEnumerable<Hotel> FromCity(string city);

        IEnumerable<Hotel> GetHotels();

    }
}
