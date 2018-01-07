ticketGamesApp.directive('logged', function () {
    return {
        restrict: "EA",
        scope: {
            message: "@",
            actions: "="
        },
        replace: false,
        templateUrl: "app/directives/logged/logged.html",
        controller: function ($scope, $rootScope, $window, $sce, globalService, accountService, cookieService) {
            var vmLogged = this;

            $scope.balance = 0;

            accountService.get(function (response) {

                $scope.balance = response.data.Balance;

                globalService.setItem('balance', response.data.Balance);
            });

            $rootScope.logged = {
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

            $scope.trustAsHtml = function (html) {

                return $sce.trustAsHtml(html);

            };

            $scope.logout = function () {

                globalService.clear();
                cookieService.clear();
                $window.location.href = "/";
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