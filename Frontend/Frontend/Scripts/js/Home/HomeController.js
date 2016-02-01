app.controller('HomeController', function ($scope, $http) {
    $scope.init = function()
    {
        $scope.ready = false;
        $scope.airports = [];
        $scope.map = { center: { latitude: 47.282949, longitude: -1.521396 }, zoom: 8 };
        $scope.airportCity = '';
   
    }
    $scope.onClickAirport = function (marker, eventName, model) {
        model.show = !model.show;
    };

    $scope.title = "Window Title!";
    $scope.loadAirports = function()
    {
        $scope.airports = [];
        $http({
            method: "GET",
            url: "/api/airports"
        }).then(function mySucces(response) {
            console.log(response);
            for (var i in response.data) {
                $scope.airports[i] = { id: response.data[i].Id, coords: { latitude: response.data[i].Lat, longitude: response.data[i].Lng }, city: response.data[i].City, name: response.data[i].Name, country: response.data[i].Country, type: 'airport' }
            }
            $scope.ready = true;
        }, function myError(response) {
            $scope.myWelcome = response.statusText;
        });
    }

    $scope.loadAirportsByCity = function(city)
    {
        if (city)
        {
            $http({
                method: "GET",
                url: "/api/airports/byCity/" + city
            }).then(function mySucces(response) {
                $scope.airports = [];
                for (var i in response.data) {
                    $scope.airports[i] = { id: response.data[i].Id, coords: { latitude: response.data[i].Lat, longitude: response.data[i].Lng }, city: response.data[i].City, name: response.data[i].Name, country: response.data[i].Country, type: 'airport', show: false }
                }
            }, function myError(response) {
                // osef
            });
        }
       
    }
    

    $scope.init();
    // $scope.loadAirports();
    $scope.ready = true;

});