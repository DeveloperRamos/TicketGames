ticketGamesApp.directive('breadcrumb', function () {
    return {
        restrict: "EA",
        scope: {
            message: "@",
            actions: "="
        },
        replace: false,
        templateUrl: "app/directives/breadcrumb/breadcrumb.html",
        controller: function ($scope, $rootScope, $window, $sce) {
            var vmbread = this;


            $rootScope.bread = {
                text: function (obj) {

                    var html = '';


                    angular.forEach(obj.pages, function (value, key) {

                        //<a href="#/">Início</a>
                        //    <span>Cart</span>


                        html = html + '<a href="#/' + value.page + '">' + value.title + '</a> ';

                    });

                    html = html + '<span>' + obj.title + '</span>';

                    $scope.html = html;

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