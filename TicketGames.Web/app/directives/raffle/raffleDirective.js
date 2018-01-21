ticketGamesApp.directive('raffle', function () {
    return {
        restrict: "EA",
        scope: {
            message: "@",
            actions: "="
        },
        replace: false,
        templateUrl: "app/directives/raffle/raffle.html",
        controller: function ($scope, $rootScope, $window, $sce, globalService, productService, cookieService) {
            //var vmLogged = this;

            $scope.balance = 0;

            var logged = cookieService.getItem('logged') ? true : false;

            $rootScope.lucky = function (productId) {

                productService.getRaffle(productId, function (response) {

                    $scope.wasLoaded = true;

                    $scope.raffle = response.data;

                    var resutl = response.data;

                });

                var estete = productId;


            };

            $rootScope.raf = {
                text: function (obj) {

                },

                show: function () {

                    if ($(".breadcrumbs").is(":visible"))
                        return true;

                    $(".breadcrumbs").show();
                    return false;
                },

                hide: function () {
                    $(".breadcrumbs").hide();
                }
            }

            //$scope.trustAsHtml = function (html) {

            //    return $sce.trustAsHtml(html);

            //};


            //$scope.actions = {
            //    show: function () {

            //        //if ($(".overlay, .bx-modal").is(":visible"))
            //        //    return true;

            //        //$(".overlay, .bx-modal").show();
            //        //return false;
            //    },

            //    hide: function () {
            //        //$(".overlay, .bx-modal").hide();
            //    },
            //};

        }
    }

});