using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using libAirports.Models;
namespace frontend.Controllers
{
    
    public class AirportController : ApiController
    {
        IAiports airports= new AiportsDb();
        [HttpGet]
        [Route("api/airports/")]
        public IEnumerable<Airport> All()
        {
            return airports.GetAirports();
        }
        [HttpGet]
        [Route("api/airports/{code}")]
        public Airport SearchByCode(String code)
        {
            return airports.SearchByCode(code);
        }
        [HttpGet]
        [Route("api/airports/search/{name}")]
        public IEnumerable<Airport> SearchByName(String name)
        {
            return airports.SearchByName(name);
        }
        [HttpGet]
        [Route("api/airports/from/{code}")]
        public IEnumerable<Airport> From(String code)
        {
            return airports.From(code);
        }
        [HttpGet]
        [Route("api/airports/to/{code}")]
        public IEnumerable<Airport> To(String code)
        {
            return airports.To(code);
        }
        [HttpGet]
        [Route("api/airports/byCity/{city}")]
        public IEnumerable<Airport> City(String city)
        {
            return airports.ByCity(city);
        }
        [HttpGet]
        [Route("api/airports/autocompleteCity/{city}")]
        public IEnumerable<String> AutocompleteCity(String city)
        {
            return airports.AutocompleteCity(city);
        }
    }
}
