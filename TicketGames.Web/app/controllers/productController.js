'use strict';

ticketGamesApp
    .controller('productController', ['$scope', '$cookieStore',
        function ($scope, $cookieStore) {
            var vmProduct = this;


            vmProduct.initialize = function () {

                $scope.actions.text("Ramos");

            };

            //initialize();
        }]);