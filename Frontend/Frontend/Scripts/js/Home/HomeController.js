app.controller('HomeController', function ($scope) {
    $scope.markers = [
        {
            id: 1,
            coords: {
                latitude: 51.4775, longitude: -0.461389
            }
        },
        {
            id: 2,
            coords: {
                latitude: 68.534444, longitude: -89.808056
            }
        },
        {
            id: 3,
            coords: {
                latitude: 28.200881, longitude: 83.982056
            }
        }
    ];
    $scope.map = { center: { latitude: 47.282949, longitude: -1.521396}, zoom: 8 };

});