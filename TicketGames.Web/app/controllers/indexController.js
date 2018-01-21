'use strict';

ticketGamesApp
    .controller('indexController', ['$scope', '$cookieStore', '$rootScope', '$location', '$routeParams', 'cartService', 'categoryService', 'participantService', 'globalService', '$window', 'cookieService',
        function ($scope, $cookieStore, $rootScope, $location, $routeParams, cartService, categoryService, participantService, globalService, $window, cookieService) {
            var vmIndex = this;

            vmIndex.sumCart = 0;


            var initialize = function () {

                //$scope.loading = true;
                var teste = $scope;

                var logged = cookieService.getItem('logged');

                vmIndex.logged = logged ? logged : false;

                getCategories();

                if ($rootScope.bread) {
                    $rootScope.bread.hide();
                }

            };
           
            var getCategories = function () {

                vmIndex.categories = cookieService.getItem('categories');

                if (!vmIndex.categories) {

                    categoryService.getCategories(function (response) {

                        vmIndex.categories = response.data;
                        cookieService.setItem('categories', response.data);
                    });
                }

            };

            vmIndex.register = function (participant) {

                if (participant.cpf && participant.email) {

                    participantService.createParticipant(participant, function (response) {

                        $window.location.reload();

                    }, function (error) {

                        var teste = error;
                    });
                }
            };

            vmIndex.login = function (participant) {

                if (participant.login && participant.password) {

                    participantService.login(participant, function (response) {

                        cookieService.setItem('token', response.data.access_token);

                        cookieService.setItem('logged', true);

                        $window.location.reload();

                    }, function (error) {

                        var teste = error;
                    });

                }
            };

            vmIndex.search = function () {

                if (vmIndex.word) {

                    if ($routeParams.cat) {
                        var CategoryId = parseInt($routeParams.cat)

                        $location.url('/Busca?cat=' + CategoryId + '&prod=' + vmIndex.word);
                    }
                    else {

                        $location.url('/Busca?prod=' + vmIndex.word);
                    }
                }
            };

            initialize();
        }]);
