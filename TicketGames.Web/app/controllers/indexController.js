'use strict';

ticketGamesApp
    .controller('indexController', ['$scope', '$cookieStore', '$rootScope', '$location', 'cartService', 'categoryService', 'participantService', 'globalService', '$window',
        function ($scope, $cookieStore, $rootScope, $location, cartService, categoryService, partcipantService, globalService, $window) {
            var vmIndex = this;



            var initialize = function () {

                //$scope.loading = true;
                var teste = $scope;

                var logged = globalService.getItem('logged');

                vmIndex.logged = logged ? logged : false;

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

                if (participant.cpf && participant.email) {

                    partcipantService.createParticipant(participant, function (response) {

                    }, function (error) {

                        var teste = error;
                    });
                }
            };

            vmIndex.login = function (participant) {

                if (participant.login && participant.password) {

                    partcipantService.login(participant, function (response) {

                        globalService.setItem('token', response.data.access_token);

                        globalService.setItem('logged', true);

                        $window.location.reload();                       

                    }, function (error) {

                        var teste = error;
                    });

                }
            };            

            initialize();
        }]);
