'use strict';

ticketGamesApp
    .controller('searchController', ['$scope', '$cookieStore', '$rootScope', '$routeParams', '$sce', 'searchService',
        function ($scope, $cookieStore, $rootScope, $routeParams, $sce, searchService) {
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

                var search = {
                    CategoryId: parseInt($routeParams.cat),
                    DepartmentId: $routeParams.dep ? $routeParams.dep : 0,
                    Word: $routeParams.prod ? $routeParams.prod : ''
                };

                searchProducts(search);

            };

            var searchProducts = function (search) {
                searchService.searchProducts(search, function (response) {
                    vmSearch.products = response.data;
                });

            };


            $scope.trustAsHtml = function (html) {

                return $sce.trustAsHtml(html);

            };

            initialize();
        }]);