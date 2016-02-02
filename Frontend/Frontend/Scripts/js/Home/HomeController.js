app.controller('HomeController', function ($scope, $http, $location, $timeout) {
    // variable initialization
    $scope.init = function ()
    {
        $scope.airportDeparture = null;
        $scope.airportArrival = null;
        $scope.airports = [];
        $scope.map = { center: { latitude: 47.282949, longitude: -1.521396 }, zoom: 8 };
        $scope.airportCity = '';
        $scope.searchingDepartureAirports = false;
        $scope.airportsLinks = [];   
    }

    // to generate a airport icon
    $scope.generateMarkerIcon = function(color)
    {
        var url = "";
        if(color === "blue")
        {
            url = '../../../fonts/icons/airport_icon_blue.png';
        } else {
            url = '../../../fonts/icons/airport_icon_green.png'
        }
        return { url: url, scaledSize: new google.maps.Size(34, 44) };
    }

    // on click on a marker symbolizing a airport
    $scope.onClickAirport = function (marker, eventName, model) {
        if (model.type === 'airportDeparture')
        {
            onClickAirportDeparture(marker, eventName, model);
        }
        else
        {
            onClickAirportArrival(marker, eventName, model);
        }
        
        
    };

    // handle the click on a departure airport 
    function onClickAirportDeparture(marker, eventName, model)
    {
        if ($scope.airportDeparture && model == $scope.airportDeparture.model) {
            marker.setIcon($scope.generateMarkerIcon('blue'));
            $scope.airportDeparture = null;
        } else {
            if ($scope.airportDeparture) {
                $scope.airportDeparture.marker.setIcon($scope.generateMarkerIcon('blue'));
            }
            marker.setIcon($scope.generateMarkerIcon('green'));
            $scope.airportDeparture = { marker: marker, model: model };
            $scope.loadAirportsByDepartureCode(model.code);
        }
    }

    // handle the click on a departure airport 
    function onClickAirportArrival(marker, eventName, model) {
        $scope.airportArrival = model;
        var toDelete = [];
        for(var i in $scope.airportsLinks)
        {
            if ($scope.airportsLinks[i].path[1] !== model.coords)
            {
                toDelete.push($scope.airportsLinks[i]);
            }
        }
        for(var i in toDelete)
        {
            $scope.airportsLinks.splice($scope.airportsLinks.indexOf(toDelete[i]), 1);
        }
        toDelete = [];
        for(var i in $scope.airports)
        {
            if ($scope.airports[i].code !== $scope.airportDeparture.model.code && $scope.airports[i].code !== $scope.airportArrival.code) {
                toDelete.push($scope.airports[i]);
            }
        }
        for (var i in toDelete) {
            $scope.airports.splice($scope.airports.indexOf(toDelete[i]), 1);
        }
        marker.setIcon($scope.generateMarkerIcon('green'));
    }
    
    // load airports according to a specified city
    $scope.loadAirportsByCity = function (city) {
        // check that the user typed something before searching
        if (city) {
            $http({
                method: "GET",
                url: "/api/airports/byCity/" + city
            }).then(function mySucces(response) {
                $scope.airports = [];
                for (var i in response.data) {
                    $scope.airports.push({ id: response.data[i].Id, coords: { latitude: response.data[i].Lat, longitude: response.data[i].Lng }, code: response.data[i].Code, city: response.data[i].City, name: response.data[i].Name, country: response.data[i].Country, type: 'airportDeparture', options: { labelClass: 'marker_labels', labelAnchor: '12 65', labelContent: response.data[i].Name, icon: $scope.generateMarkerIcon('blue') } });
                }
            }, function myError(response) {
                // osef
            });
        }

    }

    // load arrival airport based on a selected departure airport
    $scope.loadAirportsByDepartureCode = function(departureCode)
    {
        $scope.airports = [];
        $http({
            method: "GET",
            url: "/api/airports/from/" + departureCode
        }).then(function mySucces(response) {
            for (var i in response.data) {
                $scope.airports.push({ id: response.data[i].Id, coords: { latitude: response.data[i].Lat, longitude: response.data[i].Lng }, code: response.data[i].Code, city: response.data[i].City, name: response.data[i].Name, country: response.data[i].Country, type: 'airportArrival', options: { labelClass: 'marker_labels', labelAnchor: '12 65', labelContent: response.data[i].Name, icon: $scope.generateMarkerIcon('blue') } });
            }
            $scope.airports.push({ id: $scope.airportDeparture.model.id, coords: $scope.airportDeparture.model.coords, code: $scope.airportDeparture.model.code, city: $scope.airportDeparture.model.city, name: $scope.airportDeparture.model.name, country: $scope.airportDeparture.model.country, type: 'airportDeparture', options: { labelClass: 'marker_labels', labelAnchor: '12 65', labelContent: $scope.airportDeparture.model.options.labelContent, icon: $scope.generateMarkerIcon('green') } });
            $scope.generatePathBetweenAirports()
        }, function myError(response) {
            // osef
        });
    }
    
    // generate links between the departure airport and the available arrival airports
    $scope.generatePathBetweenAirports = function()
    {
        var stroke = {
            color: '#d4950d',
            weight: 2
        }
        var airportDepartureCoords = null;
        var airportsArrivalCoords = [];
        for(var i in $scope.airports)
        {
            if ($scope.airports[i].type === 'airportArrival') {
                airportsArrivalCoords.push($scope.airports[i].coords);
            } else 
            {
                airportDepartureCoords = $scope.airports[i].coords;
            }
        }
        for (var i in airportsArrivalCoords)
        {
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