'use strict';

ticketGamesApp
    .controller('paymentController', ['$scope', '$cookieStore', '$rootScope', '$routeParams', '$sce',
        function ($scope, $cookieStore, $rootScope, $routeParams, $sce) {
            var vmPayment = this;

            var initialize = function () {
                //     getProduct();
            };




            $scope.trustAsHtml = function (html) {

                return $sce.trustAsHtml(html);

            };

            initialize();
        }]);