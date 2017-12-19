'use strict';

ticketGamesApp
    .controller('homeController', ['$scope', '$cookieStore', '$rootScope', 'showcaseService', 'productService', 'cartService', 'globalService',
        function ($scope, $cookieStore, $rootScope, showcaseService, productService, cartService, globalService) {
            var vmHome = this;


            var initialize = function () {
                getShowcases();
                var logged = globalService.getItem('logged');

                vmHome.logged = logged ? logged : false;
            };

            var getShowcases = function () {
            
                vmHome.recent = globalService.getObj('recents');
            
                if(!vmHome.recent){
                    showcaseService.getShowcases(2, function (response) {

                    vmHome.recent = response.data;
                    globalService.setObj('recents', response.data);                    
                    $rootScope.showcase.show();

                    });
                }else{
                    $rootScope.showcase.show();
                }
                
                vmHome.popular = globalService.getObj('popular');

                if(!vmHome.popular){
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

                var logged = globalService.getItem('logged');

                logged = logged ? logged : false;

                if (logged) {
                    cartService.addCart(productId);
                }
                else {
                    alert('Você precisa se logar!');
                }
            };

            initialize();
        }]);