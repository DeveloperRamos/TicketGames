'use strict';

ticketGamesApp
    .controller('searchController', ['$scope', '$cookieStore', '$rootScope', '$routeParams', '$sce',
        function ($scope, $cookieStore, $rootScope, $routeParams, $sce) {
            var vmSearch = this;

            var initialize = function () {

            };


            $scope.trustAsHtml = function (html) {

                return $sce.trustAsHtml(html);

            };

            initialize();
        }]);