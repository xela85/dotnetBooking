app.controller('HomeController', function ($scope, $http) {
    // variable initialization
    $scope.init = function () {
        $scope.airportDeparture = null;
        $scope.airportArrival = null;
        $scope.airports = [];
        $scope.map = { center: { latitude: 47.282949, longitude: -1.521396 }, zoom: 8 };
        $scope.airportCity = '';
        $scope.airportsLinks = [];
        $scope.cities = [];
        $scope.autocompleteCityIsAllowed = true;

        $('.datepicker').pickadate({
            selectMonths: true, // Creates a dropdown to control month
            selectYears: 15 // Creates a dropdown of 15 years to control year
        });

    }
    $scope.handleChangeCity = function(city)
    {
        console.log(city);
        var verif = false
        for(var i in $scope.cities)
        {
            if($scope.cities[i] === city)
            {
                
                verif = true;
                break;
            }
        }
        if(verif)
        {
            console.log("verif est true");
            autocompleteCityIsAllowed = false;
            $scope.loadAirportsByCity(city);
        } else
        {
            console.log("verif est false");
            autocompleteCityIsAllowed = true;
            $scope.autocompleteCity(city);
        }
    }
    $scope.autocompleteCity = function (airportCity) {
        if ($scope.autocompleteCityIsAllowed)
        {
            if (airportCity) {
                $http({
                    method: "GET",
                    url: "/api/airports/AutocompleteCity/" + airportCity
                }).then(function mySucces(response) {
                    $scope.cities = response.data;
                    console.log($scope.cities);
                }, function myError(response) {
                    // osef
                });
            } else {
                $scope.cities = [];
            }
        }
        
    }
    function getIconUrl(color) {
        return color === "blue" ? '../../../fonts/icons/airport_icon_blue.png' : '../../../fonts/icons/airport_icon_green.png';

    }
    // to generate a airport icon
    function generateMarkerIcon(color) {
        return { url: getIconUrl(color), scaledSize: new google.maps.Size(34, 44) };
    }

    // on click on a marker symbolizing a airport
    $scope.onClickAirport = function (marker, eventName, model) {
        if (model.type === 'airportDeparture') {
            onClickAirportDeparture(marker, eventName, model);
        }
        else {
            onClickAirportArrival(marker, eventName, model);
        }
    };

    // handle the click on a departure airport 
    function onClickAirportDeparture(marker, eventName, model) {
        if ($scope.airportDeparture && model == $scope.airportDeparture.model) {
            model.icon.url = getIconUrl('blue');
            $scope.airportDeparture = null;
            $scope.airportArrival = null;
            $scope.airportsLinks = [];
            $scope.airports = [];
            // reload the airport departures according to the airportCity model
            $scope.loadAirportsByCity($scope.airportCity);
        } else {
            if ($scope.airportDeparture) {
                $scope.airportDeparture.model.icon.url = getIconUrl('blue');
            }
            model.icon.url = getIconUrl('green');
            $scope.airportDeparture = { marker: marker, model: model };
            $scope.loadAirportsByDepartureCode(model.code);
        }
    }

    // handle the click on a arrival airport 
    function onClickAirportArrival(marker, eventName, model) {
        if ($scope.airportArrival && model == $scope.airportArrival.model) {
            model.icon.url = getIconUrl('blue');
            $scope.airportArrival = null;
            $scope.airportsLinks = [];
            $scope.airports = [];
            $scope.loadAirportsByDepartureCode($scope.airportDeparture.model.code);
        } else {
            if ($scope.airportArrival) {
                $scope.airportArrival.model.icon.url = getIconUrl('blue');
            }
            model.icon.url = getIconUrl('green');
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
            $scope.airports = [];
            // add airport arrival marker
            $scope.airports.push($scope.airportArrival.model);
            // add airport departure marker
            $scope.airports.push($scope.airportDeparture.model);
        }
    }

    // load airports according to a specified city
    $scope.loadAirportsByCity = function (city) {
        $scope.airportsLinks = [];
        // check that the user typed something before searching
        if (city) {

            $http({
                method: "GET",
                url: "/api/airports/byCity/" + city
            }).then(function mySucces(response) {
                $scope.airports = [];
                for (var i in response.data) {
                    $scope.airports.push({ id: response.data[i].Id, coords: { latitude: response.data[i].Lat, longitude: response.data[i].Lng }, code: response.data[i].Code, city: response.data[i].City, name: response.data[i].Name, country: response.data[i].Country, type: 'airportDeparture', options: { labelClass: 'marker_labels', labelAnchor: '12 65', labelContent: response.data[i].Name }, icon: generateMarkerIcon('blue') });
                }
            }, function myError(response) {
                // osef
            });
        }
    }

    // load arrival airport based on a selected departure airport
    $scope.loadAirportsByDepartureCode = function (departureCode) {
        $scope.airports = [];
        $scope.airports.push($scope.airportDeparture.model);
        $http({
            method: "GET",
            url: "/api/airports/from/" + departureCode
        }).then(function mySucces(response) {
            for (var i in response.data) {
                $scope.airports.push({ id: response.data[i].Id, coords: { latitude: response.data[i].Lat, longitude: response.data[i].Lng }, code: response.data[i].Code, city: response.data[i].City, name: response.data[i].Name, country: response.data[i].Country, type: 'airportArrival', options: { labelClass: 'marker_labels', labelAnchor: '12 65', labelContent: response.data[i].Name }, icon: generateMarkerIcon('blue') });
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
        for (var i in $scope.airports) {
            if ($scope.airports[i].type === 'airportArrival') {
                airportsArrivalCoords.push($scope.airports[i].coords);
            } else {
                airportDepartureCoords = $scope.airports[i].coords;
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
    $scope.init();

});