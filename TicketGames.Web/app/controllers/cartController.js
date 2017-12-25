'use strict';

ticketGamesApp
    .controller('cartController', ['$scope', '$cookieStore', '$rootScope', '$location', '$window', 'cartService', 'globalService',
        function ($scope, $cookieStore, $rootScope, $location, $window, cartService, globalService) {
            var vmCart = this;

            vmCart.Subtotal = 0.00;
            vmCart.Total = 0.00;

            vmCart.options = [1, 2, 3, 4, 5];

            var initialize = function () {
                if ($rootScope.bread) {
                    $rootScope.bread.show();
                    $rootScope.showcase.hide();

                    var obj = {
                        "title": 'Carrinho',
                        "pages": []
                    };

                    $rootScope.bread.text(obj);
                }

                getCart();
            };


            var getCart = function () {

                cartService.get(function (response) {

                    vmCart.carts = response.data;


                    if (response.data) {
                        var log = [];
                        angular.forEach(response.data, function (value, key) {

                            vmCart.Subtotal += (value.Price * value.Quantity);
                            vmCart.Total += (value.Price * value.Quantity);

                        }, log);
                    }

                }, function (error) {

                    $location.path('/');
                    var teste = error;
                });
            };

            vmCart.remove = function (cartId) {

                cartService.remove(cartId, function (response) {

                    $window.location.reload();
                });
            };

            vmCart.update = function (productId, quantity) {

                cartService.update(productId, quantity, function (response) {
                    $window.location.reload();
                });
            };

            vmCart.next = function () {
                var logged = globalService.getItem('logged');

                logged = logged ? logged : false;

                if (logged) {
                    cartService.add(0, 1, true, function (response) {
                        $location.path('/Endereco');
                    });
                }
                else {
                    alert('Você precisa se logar!');
                }                
            };


            initialize();
        }]);