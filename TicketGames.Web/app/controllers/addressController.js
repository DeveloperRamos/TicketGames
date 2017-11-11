'use strict';

ticketGamesApp
    .controller('addressController', ['$scope', '$cookieStore', '$rootScope', '$location', 'searchService',
        function ($scope, $cookieStore, $rootScope, $location, searchService) {
            var vmAddress = this;

            var initialize = function () {


                if (!$rootScope.cart)
                    $location.path('/Carrinho');


                if ($rootScope.bread) {
                    $rootScope.bread.show();
                    $rootScope.showcase.hide();

                    var obj = {
                        "title": 'Endereço',
                        "pages": [{
                            "page": "Carrinho",
                            "title": "Carrinho"
                        }]
                    };

                    $rootScope.bread.text(obj);
                }
            };



            vmAddress.save = function (address) {

                $rootScope.cart.address = address;

                $location.path('/Pagamento');

            };

            vmAddress.search = function (valor) {

                //Nova variável "cep" somente com dígitos.
                var cep = valor.replace(/\D/g, '');

                //Verifica se campo cep possui valor informado.
                if (cep !== "") {

                    //Expressão regular para validar o CEP.
                    var validacep = /^[0-9]{8}$/;

                    searchService.searchCep(cep, function (response) {

                        $scope.address.street = response.data.logradouro;
                        $scope.address.district = response.data.bairro;
                        $scope.address.city = response.data.localidade;
                        $scope.address.state = response.data.uf;

                    });


                } //end if.
                else {
                    //cep sem valor, limpa formulário.
                    alert("Formato de CEP inválido.");
                }
            };

            initialize();
        }]);