'use strict';

ticketGamesApp
    .controller('homeController', ['$scope', '$cookieStore',
        function ($scope, $cookieStore) {
            var vmHome = this;
            

            var initialize = function () {

            };

            //var getShowcases = function () {

            //    showcaseService.getShowcases(function (data) {

            //        angular.forEach(data.data, function (value, key) {

            //            switch (value.ShowcaseType) {
            //                case 1: {
            //                    vmHome.banner = value;
            //                    break;
            //                }
            //                case 2: {
            //                    vmHome.category = value;
            //                    break;
            //                }
            //            }
            //        });

            //    }, function (error) {

            //        var errore = error;
            //    });
            //};

            $scope.random = function () {
                var tes = 0.5 - Math.random();
                var valor = (Math.floor(Math.random() * 2))
                var numb = Math.floor((Math.random() * 1000) + 1);
                return 0.5 - Math.random();
            }          


            initialize();
        }]);