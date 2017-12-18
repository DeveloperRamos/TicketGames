'use strict';

ticketGamesApp
    .controller('cartController', ['$scope', '$cookieStore', '$rootScope', '$location', 'cartService',
        function ($scope, $cookieStore, $rootScope, $location, cartSevice) {
            var vmCart = this;

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

                }, function (error) {

                    $location.path('/');
                    var teste = error;
                });
            };

            vmCart.remove = function (cartId) {

                cartSevice.removeCart(cartId, function (response) {

                    cartSevice.getCart(function (responseC) {

                        vmCart.carts = null;

                        var log = [];
                        angular.forEach(responseC.data, function (value, key) {

                            if (value.Id != cartId) {
                            }

                        }, log);

                    });



                }, function (error) {

                    $location.path('/');
                    var teste = error;
                });


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