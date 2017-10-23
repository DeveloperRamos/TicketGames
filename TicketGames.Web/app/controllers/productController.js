'use strict';

ticketGamesApp
    .controller('productController', ['$scope', '$cookieStore', '$rootScope',
        function ($scope, $cookieStore, $rootScope) {
            var vmProduct = this;


            var initialize = function () {

                if ($rootScope.bread) {
                    $rootScope.bread.show();
                    $rootScope.showcase.hide();

                    var obj = {
                        "title": 'Need for Speed Rivals',
                        "pages": [{
                            "page": "Busca?cat=" + 12,
                            "title": "Playstation 4"
                        }]
                    };

                    $rootScope.bread.text(obj);



                }
            };

            initialize();
        }]);