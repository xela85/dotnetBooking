using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using libAirports.Models;
using data.messaging;
namespace libAirports.Models
{
    public class AirportsApi : IAiports
    {
        private static String apiKey = "7ba7a4088acc0c600952f369adbd4d39";
        private static String url = "https://airport.api.aero/";

        public IEnumerable<Airport> GetAirports()
        {
            List<Airport> airports = new List<Airport>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("airport/?user_key=" + apiKey).Result;
                if (response.IsSuccessStatusCode)
                {
                    String result = response.Content.ReadAsStringAsync().Result;
                    ApiResponse reponse = JsonConvert.DeserializeObject<ApiResponse>(result);
                    if (reponse.success)
                        airports = reponse.airports;
                    else
                        throw new Exception(reponse.errorMessage);
                }
            }
            return airports;
        }
        public IEnumerable<Airport> SearchByName(String name)
        {
            List<Airport> airports = new List<Airport>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("airport/match/" + name + "/?user_key=" + apiKey).Result;
                if (response.IsSuccessStatusCode)
                {
                    String result = response.Content.ReadAsStringAsync().Result;
                    ApiResponse reponse = JsonConvert.DeserializeObject<ApiResponse>(result);
                    if (reponse.success)
                        airports = reponse.airports;
                    else
                        throw new Exception(reponse.errorMessage);
                }
            }
            return airports;
        }
        public Airport SearchByCode(String code)
        {
            Airport airport = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("airport/" + code + "/?user_key=" + apiKey).Result;
                if (response.IsSuccessStatusCode)
                {
                    String result = response.Content.ReadAsStringAsync().Result;
                    ApiResponse reponse = JsonConvert.DeserializeObject<ApiResponse>(result);
                    if (reponse.success)
                        airport = reponse.airports.FirstOrDefault();
                    else
                        throw new Exception(reponse.errorMessage);
                }
            }
            return airport;
        }



        public bool AddAirports(IEnumerable<Airport> airports)
        {
            throw new NotImplementedException();
        }


        bool IAiports.AddFlights(IEnumerable<Flight> flights)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Airport> From(string code)
        {
            throw new NotImplementedException();
        }
        IEnumerable<Airport> To(string code)
        {
            throw new NotImplementedException();
        }


        public bool FlightExists(string departureCityCode, string arrivalCityCode)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Airport> IAiports.From(string code)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Airport> IAiports.To(string code)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Airport> ByCity(String city)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<String> AutocompleteCity(String city)
        {
            throw new NotImplementedException();
        }

        bool IAiports.AddAirports(IEnumerable<Airport> airports)
        {
            throw new NotImplementedException();
        }

        bool IAiports.FlightExists(string departureCityCode, string arrivalCityCode)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Airport> IAiports.GetAirports()
        {
            throw new NotImplementedException();
        }

        Airport IAiports.SearchByCode(string code)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Airport> IAiports.SearchByName(string name)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Airport> IAiports.ByCity(string city)
        {
            throw new NotImplementedException();
        }

        Flight IAiports.Flight(int arrivalAirportId, int departureAirportId)
        {
            throw new NotImplementedException();
        }
    }

    [Serializable]
    public class ApiResponse
    {
        public int processingDurationMillis { get; set; }
        public Boolean authorisedAPI { get; set; }
        public Boolean success { get; set; }
        public String airline { get; set; }
        public String errorMessage { get; set; }
        public List<Airport> airports { get; set; }
    }
}
