﻿ticketGamesApp.directive('showcase', function () {
    return {
        restrict: "EA",
        scope: {
            message: "@"
            //actions: "="
        },
        replace: false,
        templateUrl: "app/directives/showcase/showcase.html",
        controller: function ($scope, $rootScope, $timeout, showcaseService, globalService) {
            var vmShowcase = this;
            $scope.count = 1;


            $rootScope.showcase = {
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


            $scope.increment = function () {
                $scope.$apply(function () {

                    if ($scope.count !== $scope.banner.length) {
                        var slideA = angular.element(document.querySelector('#slide-' + $scope.count));
                        slideA.removeClass('activeSlide');
                        slideA.addClass('disableSlide');

                        var slideB = angular.element(document.querySelector('#slide-' + ($scope.count + 1)));
                        slideB.addClass('activeSlide');
                        slideB.removeClass('disableSlide');

                        $scope.count++;

                    } else {

                        var slideA = angular.element(document.querySelector('#slide-' + $scope.banner.length));
                        slideA.removeClass('activeSlide');
                        slideA.addClass('disableSlide');

                        $scope.count = 1;

                        var slideB = angular.element(document.querySelector('#slide-' + $scope.count));
                        slideB.addClass('activeSlide');
                        slideB.removeClass('disableSlide');
                    }

                });
            };

            $scope.banner = globalService.getObj('banner');
        
            if(!$scope.banner){
                showcaseService.getShowcases(1, function (response) {
                    $scope.banner = response.data;
                    globalService.setObj('banner', response.data);              
                    $rootScope.showcase.show();
                    setInterval($scope.increment, 6500);

                });
            }
            else{
            $rootScope.showcase.show();
            setInterval($scope.increment, 6500);
            }           
        }
    }

});