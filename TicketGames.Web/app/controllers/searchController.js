'use strict';

ticketGamesApp
    .controller('searchController', ['$scope', '$cookieStore', '$rootScope', '$routeParams', '$sce', 'searchService', 'globalService', 'cartService', 'cookieService',
        function ($scope, $cookieStore, $rootScope, $routeParams, $sce, searchService, globalService, cartService, cookieService) {
            var vmSearch = this;

            vmSearch.hasProducts = false;

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
                    vmSearch.products = [];
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
                    //vmSearch.products = response.data;
                    $scope.products = response.data;

                    vmSearch.hasProducts = response.data.length > 0 ? true : false;

                    pagination(response.data, 8);
                });

            };

            var pagination = function (products, productsPage) {

                if (products) {

                    var pages_ = products.length / productsPage;

                    pages_ = pages_ > 1 ? Math.ceil(pages_) : 1;

                    var number = 1;

                    for (var i = 0; i < pages_; i++) {
                        vmSearch.pages.push({ number: number + i });
                    }

                    var log = [];
                    angular.forEach(products, function (value, key) {

                        if (key < productsPage) {
                            vmSearch.products.push(value);
                        }

                    }, log);

                }
            }

            $scope.trustAsHtml = function (html) {

                return $sce.trustAsHtml(html);

            };

            vmSearch.addCart = function (productId) {
                                
                var logged = cookieService.getItem('logged');

                logged = logged ? logged : false;

                if (logged) {
                    cartService.add(productId, 1, false, function (response) {

                        cartService.get(function (response) {
                            if (response.data) {
                                $rootScope.sumCart = response.data.length;
                            }
                        });

                        alert('Produto adicionado com sucesso!');
                    });
                }
                else {
                    alert('Você precisa se logar!');
                }
            };

            initialize();
        }]);