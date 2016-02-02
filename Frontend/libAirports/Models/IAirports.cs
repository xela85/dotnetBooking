using System;
using System.Collections.Generic;
namespace libAirports.Models
{
   public interface IAiports
    {
        bool AddAirports(IEnumerable<Airport> airports);
        bool AddFlights(IEnumerable<Flight> flights);
        bool FlightExists(string departureCityCode, string arrivalCityCode);
        IEnumerable<Airport> From(string code);
        IEnumerable<Airport> GetAirports();
        Airport SearchByCode(string code);
        IEnumerable<Airport> SearchByName(string name);
        IEnumerable<Airport> To(string code);
        IEnumerable<Airport> ByCity(string city);
    }
}
