ticketGamesApp.directive('menu', function () {
    return {
        restrict: "EA",
        scope: {
            message: "@",
            actions: "="
        },
        replace: false,
        templateUrl: "app/directives/menu/menu.html",
        controller: function ($scope, $rootScope, $window, $sce) {
            var vmMenu = this;


            $rootScope.menu = {
                show: function () {

                    if ($(".home-slider").is(":visible"))
                        return true;

                    $(".home-slider").show();
                    return false;
                },

                hide: function () {
                    $(".home-slider").hide();
                },
            };

            $scope.trustAsHtml = function (html) {

                return $sce.trustAsHtml(html);

            };


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