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

                    var html = '<a href="#/"><i class="icon-home"></i> Início</a>';


                    angular.forEach(obj.pages, function (value, key) {

                        //<a href="#/">Início</a>
                        //    <span>Cart</span>

                        if (value.fa) {
                            html = html + '<a href="#/' + value.page + '"><i class="' + value.fa + '"></i> ' + value.title + '</a> ';
                        } else {
                            html = html + '<a href="#/' + value.page + '">' + value.title + '</a> ';
                        }


                    });

                    if (obj.fa) {
                        html = html + '<span><i class="' + obj.fa + '"></i> ' + obj.title + '</span>';
                    } else {
                        html = html + '<span>' + obj.title + '</span>';
                    }



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