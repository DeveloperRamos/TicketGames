ticketGamesApp.directive('breadcrumb', function () {
    return {
        restrict: "EA",
        scope: {
            message: "@",
            actions: "="
        },
        replace: false,
        templateUrl: "app/directives/breadcrumb/breadcrumb.html",
        controller: function ($scope) {
            var vmbread = this;


            $scope.actions = {
                text: function (data) {
                    vmbread = "Marcio";
                }
            }


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