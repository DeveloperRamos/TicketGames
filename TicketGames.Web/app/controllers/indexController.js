'use strict';

ticketGamesApp
    .controller('indexController', ['$scope', '$cookieStore', '$rootScope', 'cartService', 'categoryService',
        function ($scope, $cookieStore, $rootScope, cartService, categoryService) {
            var vmIndex = this;



            var initialize = function () {
                getCategories();
                getCart();

                if ($rootScope.bread) {
                    $rootScope.bread.hide();
                }


            };

            var getCart = function () {


            };

            var getCategories = function () {

                categoryService.getCategories(function (response) {

                    vmIndex.categories = response.data;

                });

            }

            initialize();
        }]);
