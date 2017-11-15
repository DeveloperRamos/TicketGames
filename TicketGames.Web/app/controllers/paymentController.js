'use strict';

ticketGamesApp
    .controller('paymentController', ['$scope', '$cookieStore', '$rootScope', '$routeParams', '$sce',
        function ($scope, $cookieStore, $rootScope, $routeParams, $sce) {
            var vmPayment = this;

            var initialize = function () {
                //if (!$rootScope.cart)
                //    $location.path('/Carrinho');


                if ($rootScope.bread) {
                    $rootScope.bread.show();
                    $rootScope.showcase.hide();

                    var obj = {
                        "title": 'Pagamento',
                        "pages": [{
                            "page": "Carrinho",
                            "title": "Carrinho"
                        },
                        {
                            "page": "Endereco",
                            "title": "Endereço"
                        }]
                    };

                    $rootScope.bread.text(obj);
                }
            };




            $scope.trustAsHtml = function (html) {

                return $sce.trustAsHtml(html);

            };

            initialize();
        }]);