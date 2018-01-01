'use strict';

ticketGamesApp
    .controller('addressController', ['$scope', '$cookieStore', '$rootScope', '$location', 'searchService', 'cartService',
        function ($scope, $cookieStore, $rootScope, $location, searchService, cartService) {
            var vmAddress = this;

            vmAddress.address = {};

            var initialize = function () {

                if ($rootScope.bread) {
                    $rootScope.bread.show();
                    $rootScope.showcase.hide();

                    var obj = {
                        "title": 'Endereço',
                        "fa": 'fa fa-bars',
                        "pages": [{
                            "page": "Carrinho",
                            "title": "Carrinho",
                            "fa": "fa fa-shopping-cart"
                        }]
                    };

                    $rootScope.bread.text(obj);
                }

                getAddress();

            };

            var getAddress = function () {

                if (!$rootScope.cartId) {
                    $location.path('/Carrinho');
                } else {
                    cartService.getAddress($rootScope.cartId, function (response) {
                        vmAddress.address = response.data;
                    });
                }
            };

            vmAddress.save = function (address) {

                if (!address) {
                    $location.path('/Carrinho');
                } else {
                    cartService.addAddress(address, function (response) {
                        $location.path('/Pagamento');
                    });
                }
            };

            vmAddress.search = function (valor) {

                //Nova variável "cep" somente com dígitos.
                var cep = valor.replace(/\D/g, '');

                //Verifica se campo cep possui valor informado.
                if (cep !== "") {

                    //Expressão regular para validar o CEP.
                    var validacep = /^[0-9]{8}$/;

                    searchService.searchCep(cep, function (response) {

                        vmAddress.address.Street = response.data.logradouro;
                        vmAddress.address.District = response.data.bairro;
                        vmAddress.address.City = response.data.localidade;
                        vmAddress.address.State = response.data.uf;

                    });


                } //end if.
                else {
                    //cep sem valor, limpa formulário.
                    alert("Formato de CEP inválido.");
                }
            };

            initialize();
        }]);