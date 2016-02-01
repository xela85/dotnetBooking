using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libAirports.Models
{

        public class AiportsDb : IAiports
        {
            AirportContext db = new AirportContext();
            public IEnumerable<Airport> GetAirports()
            {
                return db.Airports;
            }

            public IEnumerable<Airport> SearchByName(string name)
            {
                return db.Airports.Where(a => a.Name.Contains(name));
            }
            public Airport SearchByCode(string code)
            {
                return db.Airports.FirstOrDefault(a => a.Code == code);
            }
            public IEnumerable<Airport> From(String code)
            {

                return db.Flights.Where(x => x.DepartureAirport.Code == code).Select(a => a.ArrivalAirport);
            }
            public IEnumerable<Airport> To(String code)
            {
                return db.Flights.Where(x => x.ArrivalAirport.Code == code).Select(a => a.DepartureAirport);
            }

            public IEnumerable<Airport> ByCity(String city)
            {
                return db.Airports.Where(x => x.City.ToLower().Contains(city.ToLower()));
            }

        public bool FlightExists(string departureCityCode, string arrivalCityCode)
            {
                return db.Flights.Count(f =>
                    f.DepartureAirport.Code == departureCityCode
                    && f.ArrivalAirport.Code == arrivalCityCode
                    )>0;
            }

            public bool AddAirports(IEnumerable<Airport> airports)
            {
                try
                {
                    IEnumerable<Airport> toInsert = airports.Where(a => db.Airports.Count(x => x.Code == a.Code) == 0);
                    db.Airports.AddRange(toInsert);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
            public bool AddFlights(IEnumerable<Flight> flights)
            {
                try
                {
                    IEnumerable<Flight> toInsert = flights.Where(x => !FlightExists(x.DepartureAirport.Code, x.DepartureAirport.Code));
                    db.Flights.AddRange(flights);
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        
    }
}
