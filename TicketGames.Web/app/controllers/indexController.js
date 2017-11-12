'use strict';

ticketGamesApp
    .controller('indexController', ['$scope', '$cookieStore', '$rootScope', '$location', 'cartService', 'categoryService',
        function ($scope, $cookieStore, $rootScope, $location, cartService, categoryService) {
            var vmIndex = this;



            var initialize = function () {

                //$scope.loading = true;
                var teste = $scope;
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

            };

            vmIndex.register = function () {               

                $location.path('/Cadastro');
            };

            initialize();
        }]);
