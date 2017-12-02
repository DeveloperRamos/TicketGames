'use strict';

ticketGamesApp
    .controller('registerController', ['$scope', '$cookieStore', '$rootScope', '$routeParams', '$location', 'searchService',
        function ($scope, $cookieStore, $rootScope, $routeParams, $location, searchService) {
            var vmRegister = this;

            var initialize = function () {

                if (!$routeParams.session)
                    $location.path('/');

                if ($rootScope.bread) {
                    $rootScope.bread.show();
                    $rootScope.showcase.hide();

                    var obj = {
                        "title": 'Cadastro',
                        "pages": []
                    };

                    $rootScope.bread.text(obj);
                }
            };

            vmRegister.search = function (valor) {

                //Nova variável "cep" somente com dígitos.
                var cep = valor.replace(/\D/g, '');

                //Verifica se campo cep possui valor informado.
                if (cep !== "") {

                    //Expressão regular para validar o CEP.
                    var validacep = /^[0-9]{8}$/;

                    searchService.searchCep(cep, function (response) {

                        $scope.participant.address.street = response.data.logradouro;
                        $scope.participant.address.district = response.data.bairro;
                        $scope.participant.address.city = response.data.localidade;
                        $scope.participant.address.state = response.data.uf;

                    });


                } //end if.
                else {
                    //cep sem valor, limpa formulário.
                    alert("Formato de CEP inválido.");
                }
            };


            vmRegister.save = function (register) {

                var teste = register;

            };

            initialize();
        }]);