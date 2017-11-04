'use strict';

ticketGamesApp
    .controller('searchController', ['$scope', '$cookieStore', '$rootScope', '$routeParams', '$sce',
        function ($scope, $cookieStore, $rootScope, $routeParams, $sce) {
            var vmSearch = this;

            var initialize = function () {

                if ($rootScope.bread) {
                    $rootScope.bread.show();
                    $rootScope.showcase.hide();

                    var obj = {
                        "title": "Pesquisa",
                        "pages": []
                    };

                    $rootScope.bread.text(obj);
                }

            };


            $scope.trustAsHtml = function (html) {

                return $sce.trustAsHtml(html);

            };

            initialize();
        }]);