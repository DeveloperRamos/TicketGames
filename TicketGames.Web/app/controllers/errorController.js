'use strict';

ticketGamesApp
    .controller('errorController', ['$scope', '$cookieStore', '$rootScope', 'globalService',
        function ($scope, $cookieStore, $rootScope, globalService) {
            var vmError = this;

            var initialize = function () {

                var logged = globalService.getItem('logged');

                vmError.logged = logged ? logged : false;

                if ($rootScope.bread) {
                    $rootScope.bread.show();
                    $rootScope.showcase.hide();

                    var obj = {
                        "title": 'Error',
                        "fa": 'fa fa-male',
                        "pages": []
                    };

                    $rootScope.bread.text(obj);
                }
            };


            initialize();
        }]);