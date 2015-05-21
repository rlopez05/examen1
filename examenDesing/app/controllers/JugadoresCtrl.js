angular.module('JugadoresController', [])
.controller('JugadorCtrl', ['$scope', '$http', function ($scope, $http) {

    $scope.model = {};
    $scope.accion = 'nuevo';
    $scope.MostrarControles = false;
    $scope.Jugador = {};

    $scope.limpiar = function () {
        $scope.accion = 'nuevo';
        $scope.MostrarControles = false;
        $scope.Jugador = {};
        
    };
    

    $http.get('/Jugadors/Cargar').success(function (data) {
        $scope.model = data;
    });


    $scope.MostrarCtrl=function(mostrar)
    {
        $scope.MostrarControles = true;
    };

    $scope.Editar = function (JugadorEditar) {
        $scope.accion = 'edit';
        $scope.Jugador = JugadorEditar;
        $scope.MostrarControles = true;
    }


    $scope.CrearActualizar = function () {
        if ($scope.accion == 'nuevo') {

            $http.post('/Jugadors/Create', $scope.Jugador).success(function (data) {
                $scope.model.push(data);
            });
        } else if ($scope.accion == 'edit') {
            $http.post('/Jugadors/update', $scope.Jugador);
        }
        $scope.limpiar();
    };

    $scope.Remove = function (jugador) {
        var response = $http({
            method: "post",
            url: '/Jugadors/Remove',
            params: { id: JSON.stringify(jugador.idJugador) }
        });

        var index = $scope.model.indexOf(jugador);
        $scope.model.splice(index, 1);
    };


}]);