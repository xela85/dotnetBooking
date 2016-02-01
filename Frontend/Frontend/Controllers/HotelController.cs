using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using libHotels.Models;
namespace frontend.Controllers
{
    
    public class HotelController : ApiController
    {
        IHotels hotels= new HotelDb();
        [HttpGet]
        [Route("api/hotels/")]
        public IEnumerable<Hotel> All()
        {
            return hotels.GetHotels();
        }
        [HttpGet]
        [Route("api/hotels/{city}")]
        public IEnumerable<Hotel> SearchByCity(String city)
        {
            return hotels.FromCity(city);
        }
    }
}
