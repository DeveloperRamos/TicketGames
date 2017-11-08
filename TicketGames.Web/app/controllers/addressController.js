'use strict';

ticketGamesApp
    .controller('addressController', ['$scope', '$cookieStore', '$rootScope',
        function ($scope, $cookieStore, $rootScope) {
            var vmAddress = this;

            var initialize = function () {
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

                var teste = address;

            };





            initialize();
        }]);