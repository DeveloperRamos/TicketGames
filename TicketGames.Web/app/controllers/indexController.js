'use strict';

ticketGamesApp
    .controller('indexController', ['$scope', '$cookieStore', '$rootScope', 'cartService',
        function ($scope, $cookieStore, $rootScope, cartService) {
            var vmIndex = this;



            var initialize = function () {
                getCart();


                if ($rootScope.bread) {
                    $rootScope.bread.hide();
                }


            };

            var getCart = function () {


            };

            initialize();
        }]);