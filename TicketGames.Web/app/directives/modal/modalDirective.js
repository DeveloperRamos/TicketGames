ticketGamesApp.directive('modal', function () {
    return {
        restrict: "EA",
        scope: {
            title: '=modalTitle',
            header: '=modalHeader',
            body: '=modalBody',
            footer: '=modalFooter',
            callbackbuttonleft: '&ngClickLeftButton',
            callbackbuttonright: '&ngClickRightButton',
            handler: '=myHandler'
        },
        replace: false,
        transclude: true,
        templateUrl: "app/directives/model/model.html",
        controller: function ($scope, $rootScope, $window, $sce) {
            var vmModel = this;
            $scope.handler = 'pop';

            $rootScope.bread = {
                text: function (obj) {

                    var html = '<a href="#/">Início</a>';


                    angular.forEach(obj.pages, function (value, key) {

                        //<a href="#/">Início</a>
                        //    <span>Cart</span>


                        html = html + '<a href="#/' + value.page + '">' + value.title + '</a> ';

                    });

                    html = html + '<span>' + obj.title + '</span>';

                    $scope.html = html;

                }
            }

            $scope.trustAsHtml = function (html) {

                return $sce.trustAsHtml(html);

            };           

        }
    }

});