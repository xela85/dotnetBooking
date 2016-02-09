using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using libAirports.Models;
using libHotels.Models;
using data.messaging;
namespace ReservationsSite.Controllers
{
    public class AdminController : Controller
    {

        public ActionResult Index()
        {

            return View();
        }
        public ActionResult AddAirports()
        {
            try
            {
                AirportsApi api = new AirportsApi();
                var aiports = api.GetAirports();
                libAirports.Models.AiportsDb db = new libAirports.Models.AiportsDb();
                bool done = db.AddAirports(aiports);
                if(done)
                    ViewBag.Message = "Les aéroports ont bien été ajoutés";
                else
                    ViewBag.Message = "Erreur dans l'ajout des aéroports";
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
            }

            return View("~/Views/Admin/Index.cshtml");
        }
        public ActionResult AddHotels()
        {
            try
            {
                AiportsDb AirportDb = new AiportsDb();
                List<Airport> airports = AirportDb.GetAirports().Where(x => x.City!=null).ToList();
                HotelDb HotelDb = new HotelDb();
                List<Hotel> hotels = new List<Hotel>();
                for(int i =0;i<airports.Count;i++)
                {
                    Airport a = airports[i];
                    Hotel h  = new Hotel();
                    h.City = a.City;
                    h.Lat = a.Lat;
                    h.Long = a.Lng;
                    h.Name = "Hotel " + i;
                    hotels.Add(h);
                }
                HotelDb.AddHotels(hotels);
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
            }
            return View("~/Views/Admin/Index.cshtml");
        }
        public ActionResult AddFlights()
        {
            try
            {
                AiportsDb Airports = new AiportsDb();
                var AirportsList = Airports.GetAirports().ToList(); ;
                int nbAirports = AirportsList.Count;
                List<Flight> FlightsList = new List<Flight>();
                Random rnd = new Random();
                int i = 0;
                int nbFlights = 20000;
                while (i < nbFlights)
                {
                    int departureIdx = rnd.Next(0, nbAirports);
                    int arrivalIdx = rnd.Next(0, nbAirports);
                    int DepartureAirportId = AirportsList[departureIdx].Id;
                    int ArrivalAirportId = AirportsList[arrivalIdx].Id;
                    while (departureIdx == arrivalIdx)
                        arrivalIdx = rnd.Next(0, nbAirports);
                    Flight f = new Flight()
                    {
                        DepartureAirportId = DepartureAirportId,
                        ArrivalAirportId = ArrivalAirportId
                    };
                   Flight f2 = new Flight()
                    {
                        DepartureAirportId = ArrivalAirportId,
                        ArrivalAirportId = DepartureAirportId
                    };
                  if(!FlightsList.Contains(f))
                  {
                      FlightsList.Add(f);
                      FlightsList.Add(f2);
                      i++;
                  }               
                }
               bool done = Airports.AddFlights(FlightsList);
               if (done)
                   ViewBag.Message = nbFlights * 2 + " vols ont été ajoutés";
               else
                   ViewBag.Message = "Erreur dans l'ajout des vols";
          

            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
            }
            return View("~/Views/Admin/Index.cshtml");
        }
    }
}
