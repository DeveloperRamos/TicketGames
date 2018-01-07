'use strict';

ticketGamesApp
    .controller('homeController', ['$scope', '$cookieStore', '$rootScope', 'showcaseService', 'productService', 'cartService', 'globalService', 'cookieService',
        function ($scope, $cookieStore, $rootScope, showcaseService, productService, cartService, globalService, cookieService) {
            var vmHome = this;


            var initialize = function () {
                getShowcases();
                
                var logged = cookieService.getItem('logged');

                vmHome.logged = logged ? logged : false;
            };

            var getShowcases = function () {

                vmHome.recent = globalService.getObj('recents');

                if (!vmHome.recent) {
                    showcaseService.getShowcases(2, function (response) {

                        vmHome.recent = response.data;
                        globalService.setObj('recents', response.data);
                        //$rootScope.showcase.show();

                    });
                }

                vmHome.popular = globalService.getObj('popular');

                if (!vmHome.popular) {
                    showcaseService.getShowcases(3, function (response) {

                        vmHome.popular = response.data;
                        globalService.setObj('popular', response.data);

                    });
                }
            };

            vmHome.getRaffle = function (productId) {

                if (productId) {

                    productService.getRaffle(productId, function (response) {

                        vmHome.raffle = response.data;
                        return true;
                    });
                }

            };


            $scope.random = function () {
                var tes = 0.5 - Math.random();
                var valor = (Math.floor(Math.random() * 2))
                var numb = Math.floor((Math.random() * 1000) + 1);
                return 0.5 - Math.random();
            };


            vmHome.addCart = function (productId) {
                                
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