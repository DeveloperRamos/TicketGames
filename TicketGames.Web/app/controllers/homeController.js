'use strict';

ticketGamesApp
    .controller('homeController', ['$scope', '$cookieStore', 'showcaseService',
        function ($scope, $cookieStore, showcaseService) {
            var vmHome = this;


            var initialize = function () {
                getShowcases();
            };

            var getShowcases = function () {

                var recent = showcaseService.getShowcases('Recent');

                vmHome.recent = recent;

                var popular = showcaseService.getShowcases('Popular');

                vmHome.popular = popular;
            };

            $scope.random = function () {
                var tes = 0.5 - Math.random();
                var valor = (Math.floor(Math.random() * 2))
                var numb = Math.floor((Math.random() * 1000) + 1);
                return 0.5 - Math.random();
            }


            initialize();
        }]);