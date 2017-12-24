'use strict';

ticketGamesApp
    .controller('cartController', ['$scope', '$cookieStore', '$rootScope', '$location', '$window', 'cartService',
        function ($scope, $cookieStore, $rootScope, $location, $window, cartSevice) {
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

                cartSevice.getCart(function (response) {

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

                cartSevice.removeCart(cartId, function (response) {

                    $window.location.reload();
                });
            };

            vmCart.update = function (productId, quantity) {
                var prod = productId;
                var quant = quantity;
            };

            vmCart.next = function () {
                $rootScope.cart = {};

                $rootScope.cart = {
                    cartId: 21,
                    address: {}
                };

                $location.path('/Endereco');
            };


            initialize();
        }]);