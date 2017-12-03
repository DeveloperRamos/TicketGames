'use strict';

ticketGamesApp
    .controller('registerController', ['$scope', '$cookieStore', '$rootScope', '$routeParams', '$location', 'searchService', 'participantService',
        function ($scope, $cookieStore, $rootScope, $routeParams, $location, searchService, participantService) {
            var vmRegister = this;

            var initialize = function () {
                vmRegister.participant = {};


                if (!$routeParams.session)
                    $location.path('/');

                if ($routeParams.session) {
                    $scope.session = $routeParams.session;

                    getParticipantBySession($scope.session);
                }

                if ($rootScope.bread) {
                    $rootScope.bread.show();
                    $rootScope.showcase.hide();

                    var obj = {
                        "title": 'Cadastro',
                        "pages": []
                    };

                    $rootScope.bread.text(obj);
                }
            };

            vmRegister.search = function (valor) {

                //Nova variável "cep" somente com dígitos.
                var cep = valor.replace(/\D/g, '');

                //Verifica se campo cep possui valor informado.
                if (cep !== "") {

                    //Expressão regular para validar o CEP.
                    var validacep = /^[0-9]{8}$/;

                    searchService.searchCep(cep, function (response) {

                        vmRegister.participant.Street = response.data.logradouro;
                        vmRegister.participant.District = response.data.bairro;
                        vmRegister.participant.City = response.data.localidade;
                        vmRegister.participant.State = response.data.uf;

                    });


                } //end if.
                else {
                    //cep sem valor, limpa formulário.
                    alert("Formato de CEP inválido.");
                }
            };


            vmRegister.save = function (register) {


                participantService.createParticipant(register, function (response) {
                    $location.path('/');
                });

            };


            var getParticipantBySession = function (session) {

                participantService.getParticipantBySession(session, function (response) {

                    vmRegister.participant = response.data;
                    vmRegister.participant.session = $scope.session;

                }, function (error) {

                    $location.path('/');

                });
            };

            initialize();
        }]);