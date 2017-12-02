'use strict';

ticketGamesApp
    .controller('indexController', ['$scope', '$cookieStore', '$rootScope', '$location', 'cartService', 'categoryService', 'participantService',
        function ($scope, $cookieStore, $rootScope, $location, cartService, categoryService, partcipantService) {
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

            vmIndex.register = function (participant) {

                if (participant.CPF && participant.Email) {

                    partcipantService.createParticipant(participant, function (response) {





                    }, function (error) {

                        var teste = error;
                    });
                }
            };

            initialize();
        }]);
