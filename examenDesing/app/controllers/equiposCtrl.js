angular.module('EquiposController', [])
    .controller('EquiposCtrl', ['$scope','$http', function ($scope,$http) {
        
        $scope.modeloEquipos = {};
        $scope.MostrarControles = false;
        $scope.accion = 'nuevo';
        $scope.EquipoObjeto = {};

        $http.get('/Equipos/CargarEquipos').success(function (data) {
            $scope.modeloEquipos = data; 
        });
       

        $scope.limpiar = function()
        {
            $scope.MostrarControles = false;
            $scope.accion = 'nuevo';
            $scope.EquipoObjeto = {};
        };

        $scope.MostrarCtrl = function (mostrar) {
            $scope.MostrarControles = true;
        };

        $scope.CrearActualizar = function () {
            if ($scope.accion == 'nuevo') {

                $http.post('/Equipos/Create',$scope.EquipoObjeto).success(function (data) {
                    $scope.modeloEquipos.push(data);
                });
            } else if ($scope.accion == 'edit') {
                $http.post('/Equipos/update', $scope.EquipoObjeto);
            }
            $scope.limpiar();
        };

        $scope.Editar=function(EquipoEditar)
        {
            $scope.accion = 'edit';
            $scope.EquipoObjeto = EquipoEditar;
            $scope.MostrarControles = true;
        }

        $scope.Remove = function (equipo) {
            var response = $http({
                method: "post",
                url: '/Equipos/Remove',
                params: { id: JSON.stringify(equipo.idEquipo) }
            });

            var index = $scope.modeloEquipos.indexOf(equipo);
            $scope.modeloEquipos.splice(index, 1);
        };

    }]);