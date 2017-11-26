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

                    vmSearch.pages = [];
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
                    pagination(response.data.length, 8);
                });

            };

            var pagination = function (qtdProducts, productsPage) {

                var pages_ = qtdProducts / productsPage;

                pages_ = pages_ > 1 ? pages_ : 1;

                var number = 1;

                for (var i = 0; i < pages_; i++) {
                    vmSearch.pages.push({ number: number + i });
                }
            }


            $scope.trustAsHtml = function (html) {

                return $sce.trustAsHtml(html);

            };

            initialize();
        }]);