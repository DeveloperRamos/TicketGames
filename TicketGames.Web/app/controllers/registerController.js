'use strict';

ticketGamesApp
    .controller('registerController', ['$scope', '$cookieStore', '$rootScope',
        function ($scope, $cookieStore, $rootScope) {
            var vmRegister = this;

            var initialize = function () {
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



            vmRegister.save = function (register) {

                var teste = register;

            };

            initialize();
        }]);