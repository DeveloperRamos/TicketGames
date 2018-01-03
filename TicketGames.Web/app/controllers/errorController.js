'use strict';

ticketGamesApp
    .controller('errorController', ['$scope', '$cookieStore', '$rootScope', 'globalService', 'cookieService',
        function ($scope, $cookieStore, $rootScope, globalService, cookieService) {
            var vmError = this;

            var initialize = function () {
                
                var logged = cookieService.getItem('logged');

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