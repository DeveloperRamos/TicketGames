'use strict';

ticketGamesApp
    .controller('successController', ['$scope', '$cookieStore', '$rootScope', 'globalService',
        function ($scope, $cookieStore, $rootScope, globalService) {
            var vmSuccess = this;

            var initialize = function () {

                var logged = globalService.getItem('logged');

                vmSuccess.logged = logged ? logged : false;

                if ($rootScope.bread) {
                    $rootScope.bread.show();
                    $rootScope.showcase.hide();

                    var obj = {
                        "title": 'Sucesso',
                        "pages": []
                    };

                    $rootScope.bread.text(obj);
                }
            };


            initialize();
        }]);