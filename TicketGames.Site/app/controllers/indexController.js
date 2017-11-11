'use strict';

ticketGamesApp
    .controller('indexController', ['$scope', '$cookieStore', '$rootScope',
        function ($scope, $cookieStore, $rootScope) {
            var vmIndex = this;



            vmIndex.inicio = function () {
                var a = 1;
                var b = 2;
                var result = a + b;
            };





            var initialize = function () {

                //$scope.loading = true;
                var teste = $scope;
                getCategories();
                getCart();

                if ($rootScope.bread) {
                    $rootScope.bread.hide();
                }


            };

            var getCart = function () {


            };

            var getCategories = function () {



            }

            initialize();
        }]);
