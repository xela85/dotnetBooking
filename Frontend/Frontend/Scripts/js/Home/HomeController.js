app.controller('HomeController', function ($scope, $http) {
    // variable initialization
    $scope.init = function () {
        // to save the data model and marker of the airport departure
        $scope.airportDeparture = null;
        // to save the data model and marker of the airport departure
        $scope.airportArrival = null;
        // the array containing all the markers
        $scope.markers = [];
        // our map
        $scope.map = { center: { latitude: 47.282949, longitude: -1.521396 }, zoom: 8 };
        // binded to the searchbox
        $scope.departureCity = '';
        // array containing all the links between the departure airport and the available arrival airports
        $scope.airportsLinks = [];
        // cities loaded with autocomplete
        $scope.cities = [];
        // to know if we have to do the autocomplete
        $scope.autocompleteCityIsAllowed = true;
        // initialize the datepickers
        $('.datepicker').pickadate({
            selectMonths: true, // Creates a dropdown to control month
            selectYears: 15 // Creates a dropdown of 15 years to control year
        });
        $scope.departureDate = '';
        $scope.arrivalDate = '';
        $scope.travelReservation = {
            user: {},
            hotel: {},
            flight: {},
            hotelArrivalDate: new Date(),
            hotelDepartureDate: new Date()
        };
	
        $scope.hotelNightsNumber = 0;
        $scope.hotelArrivalDate = "";
    }

    // handle the change when the user searches a city
    $scope.handleChangeCity = function (city) {
        $scope.departureCity = city;
    }
    //Used to watch the departure city changing
    $scope.$watch('departureCity', function (newValue, oldValue) {
        if (newValue === $scope.cities[0])//if the searched city exist
        {
            $scope.cities = [];
            $scope.loadAirportsByCity(newValue);
        }
        else
        {
            resetTravel();
                $http({
                    method: "GET",
                    url: $scope.urlPrefix + "api/airports/cities/" + $scope.departureCity
                }).then(function mySucces(response) {
                    if (response.data)
                        $scope.cities = response.data.splice(0, 4);

                }, function myError(response) {
                    // osef
                });
        }
    });
    $scope.book = function () {
        var flightRequest = $http.get($scope.urlPrefix + 'api/flight/' + $scope.airportDeparture.model.id + '/' + $scope.airportArrival.model.id);
        flightRequest.then(function success(response) {
            $scope.travelReservation.flight = response.data;
            var bookRequest = $http.post($scope.urlPrefix + 'api/book/', JSON.stringify($scope.travelReservation));
            bookRequest.then(function (response) {
                $('#modal-reservation').closeModal();
                Materialize.toast(response.data.Message, 3000, 'rounded')
                
            });
        });

        
    }
    $scope.changeNightsNumber = function () {
        $scope.travelReservation.hotelDepartureDate = $scope.travelReservation.hotelArrivalDate.addDays($scope.hotelNightsNumber);
    };
    $scope.openModalReservation = function()
    {
        $('#modal-reservation').openModal();
    }
    // return the url of the marker airport according to a color
    function getIconUrl(color, type) {
        return '../../../fonts/icons/' + type + '_icon_' + color + '.png';
    }

    // to generate a airport icon
    function generateMarkerIcon(color, type) {
        return { url: getIconUrl(color, type), scaledSize: new google.maps.Size(34, 44) };
    }

    // on click on a marker symbolizing a airport
    $scope.onClickAirport = function (marker, eventName, model) {
        if (model.type === 'airportDeparture') {
            onClickAirportDeparture(marker, eventName, model);
        }
        else if (model.type === 'airportArrival') {
            onClickAirportArrival(marker, eventName, model);
        } else {
            onClickHotel(marker, eventName, model);
        }
    };
    function resetTravel()
    {
        $scope.markers = [];
        $scope.airportDeparture = null;
        $scope.airportArrival = null;
        $scope.airportsLinks = [];
        $scope.hotel = null;
    }
    // handle the click on a departure airport 
    function onClickAirportDeparture(marker, eventName, model) {
        if ($scope.airportDeparture && model == $scope.airportDeparture.model) {
            model.icon.url = getIconUrl('blue', 'airport');
            $scope.airportDeparture = null;
            $scope.airportArrival = null;
            $scope.airportsLinks = [];
            $scope.markers = [];
            // reload the airport departures according to the departureCity model
            $scope.loadAirportsByCity($scope.departureCity);
        } else {
            if ($scope.airportDeparture) {
                $scope.airportDeparture.model.icon.url = getIconUrl('blue', 'airport');
            }
            model.icon.url = getIconUrl('green', 'airport');
            $scope.airportDeparture = { marker: marker, model: model };
            $scope.loadAirportsByDepartureCode(model.code);
        }
    }

    // handle the click on a arrival airport 
    function onClickAirportArrival(marker, eventName, model) {
        if ($scope.airportArrival && model == $scope.airportArrival.model) {
            model.icon.url = getIconUrl('blue', 'airport');
            $scope.airportArrival = null;
            $scope.airportsLinks = [];
            $scope.markers = [];
            $scope.loadAirportsByDepartureCode($scope.airportDeparture.model.code);
        } else {
            if ($scope.airportArrival) {
                $scope.airportArrival.model.icon.url = getIconUrl('blue', 'airport');
            }
            model.icon.url = getIconUrl('green', 'airport');
            $scope.airportArrival = { marker: marker, model: model };
            // retrieve the index of the link to keep between airport departure and airport arrival
            var index = $scope.airportsLinks.map(function (e) { return e.path[1]; }).indexOf(model.coords);
            // the link found thanks to the index below
            var airportLink = $scope.airportsLinks[index];
            // reset all links
            $scope.airportsLinks = [];
            // add our link
            $scope.airportsLinks.push(airportLink);
            // reset all airports markers
            $scope.markers = [];
            // add airport arrival marker
            $scope.markers.push($scope.airportArrival.model);
            // add airport departure marker
            $scope.markers.push($scope.airportDeparture.model);
            $scope.loadHotelsByCity(model.city);
            $scope.flightPrice = generatePrice(200, 500);
        }
    }

    // handle the click on a hotel
    function onClickHotel(marker, eventName, model) {
        if ($scope.hotel && model == $scope.hotel.model) {
            model.icon.url = getIconUrl('blue', 'hotel');
            $scope.markers.splice($scope.markers.map(function (e) { return e.coords; }).indexOf(model.coords), 1);
            $scope.loadHotelsByCity($scope.airportArrival.model.city);
        } else {
            if ($scope.hotel) {
                $scope.hotel.model.icon.url = getIconUrl('blue', 'hotel');
                $scope.markers.splice($scope.markers.map(function (e) { return e.coords; }).indexOf($scope.hotel.model.coords), 1);
            }
            model.icon.url = getIconUrl('green', 'hotel');
            $scope.hotel = { marker: marker, model: model };
            $scope.travelReservation.hotel = model.hotelObject;
            $scope.hotelNightsPrice = generatePrice(50, 100);
            // add airport arrival marker
            $scope.markers.push(model);
        }
    }
    function generatePrice(min,max)
    {
        return Math.floor(Math.random() * max) + min
    }
    
    // load airports according to a specified city
    $scope.loadAirportsByCity = function (city) {
        $scope.airportsLinks = [];
        // check that the user typed something before searching
        if (city) {

            $http({
                method: "GET",
                url: $scope.urlPrefix +"api/airports/byCity/" + city
            }).then(function mySucces(response) {
                $scope.markers = [];
                for (var i in response.data) {
                    $scope.markers.push({ id: response.data[i].Id, coords: { latitude: response.data[i].Lat, longitude: response.data[i].Lng }, code: response.data[i].Code, city: response.data[i].City, name: response.data[i].Name, country: response.data[i].Country, type: 'airportDeparture', options: { labelClass: 'marker_labels', labelAnchor: '12 65', labelContent: response.data[i].Name }, icon: generateMarkerIcon('blue', 'airport') });
                }
            }, function myError(response) {
                // osef
            });
        }
    }

    // load arrival airport based on a selected departure airport
    $scope.loadAirportsByDepartureCode = function (departureCode) {
        $scope.markers = [];
        $scope.markers.push($scope.airportDeparture.model);
        $http({
            method: "GET",
            url: $scope.urlPrefix +"api/airports/from/" + departureCode
        }).then(function mySucces(response) {
            for (var i in response.data) {
                $scope.markers.push({ id: response.data[i].Id, coords: { latitude: response.data[i].Lat, longitude: response.data[i].Lng }, code: response.data[i].Code, city: response.data[i].City, name: response.data[i].Name, country: response.data[i].Country, type: 'airportArrival', options: { labelClass: 'marker_labels', labelAnchor: '12 65', labelContent: response.data[i].Name }, icon: generateMarkerIcon('blue', 'airport') });
            }
            $scope.generatePathBetweenAirports()
        }, function myError(response) {
            // osef
        });
    }

    // generate links between the departure airport and the available arrival airports
    $scope.generatePathBetweenAirports = function () {
        var stroke = {
            color: '#d4950d',
            weight: 2
        }
        var airportDepartureCoords = null;
        var airportsArrivalCoords = [];
        for (var i in $scope.markers) {
            if ($scope.markers[i].type === 'airportArrival') {
                airportsArrivalCoords.push($scope.markers[i].coords);
            } else if ($scope.markers[i].type === 'airportDeparture') {
                airportDepartureCoords = $scope.markers[i].coords;
            }
        }
        for (var i in airportsArrivalCoords) {
            $scope.airportsLinks.push({
                path: [airportDepartureCoords, airportsArrivalCoords[i]], stroke: stroke, icons: [{
                    icon: {
                        path: google.maps.SymbolPath.FORWARD_OPEN_ARROW
                    },
                    offset: '25px',
                    repeat: '50px'
                }]
            })
        }
    }

    // load hotel around an arrival airport
    $scope.loadHotelsByCity = function (city) {
        $http({
            method: "GET",
            url: $scope.urlPrefix + "api/hotels/" + city
        }).then(function mySucces(response) {
            for (var i in response.data) {
                $scope.markers.push({
                    id: response.data[i].Id,
                    coords: {
                        latitude: response.data[i].Lat,
                        longitude: response.data[i].Long
                    },
                    city: response.data[i].City,
                    name: response.data[i].Name,
                    type: 'hotel',
                    hotelObject : response.data[i],
                    options: { labelClass: 'marker_labels', labelAnchor: '12 65', labelContent: response.data[i].Name }, icon: generateMarkerIcon('blue', 'hotel')
                });
            }
        }, function myError(response) {
            // osef
        });
    }
    $scope.init();

});
Date.prototype.addDays = function(days)
{
    var dat = new Date(this.valueOf());
    dat.setDate(dat.getDate() + days);
    return dat;
}