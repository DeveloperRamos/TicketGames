'use strict';

ticketGamesApp
    .controller('homeController', ['$scope', '$cookieStore',
        function ($scope, $cookieStore) {
            var vmHome = this;


            var initialize = function () {
                getShowcases();
            };

            var getShowcases = function () {


                //showcaseService.getShowcases(2, function (response) {

                //    vmHome.recent = response.data;

                //});

                //showcaseService.getShowcases(3, function (response) {

                //    vmHome.popular = response.data;

                //});
            };

            $scope.random = function () {
                var tes = 0.5 - Math.random();
                var valor = (Math.floor(Math.random() * 2))
                var numb = Math.floor((Math.random() * 1000) + 1);
                return 0.5 - Math.random();
            };


            vmHome.addCart = function () {



            };


            initialize();
        }]);