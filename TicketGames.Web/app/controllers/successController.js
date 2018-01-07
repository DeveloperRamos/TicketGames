'use strict';

ticketGamesApp
    .controller('successController', ['$scope', '$cookieStore', '$rootScope', 'globalService', 'cookieService',
        function ($scope, $cookieStore, $rootScope, globalService, cookieService) {
            var vmSuccess = this;

            var initialize = function () {
                                
                var logged = cookieService.getItem('logged');

                vmSuccess.logged = logged ? logged : false;

                if ($rootScope.bread) {
                    $rootScope.bread.show();
                    $rootScope.showcase.hide();

                    var obj = {
                        "title": 'Sucesso',
                        "fa": 'fa fa-child',
                        "pages": []
                    };

                    $rootScope.bread.text(obj);
                }
            };


            initialize();
        }]);