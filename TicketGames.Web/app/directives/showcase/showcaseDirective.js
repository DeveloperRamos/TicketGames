ticketGamesApp.directive('showcase', function () {
    return {
        restrict: "EA",
        scope: {
            message: "@"
            //actions: "="
        },
        replace: false,
        templateUrl: "app/directives/showcase/showcase.html",
        controller: function ($scope, $rootScope, showcaseService) {
            var vmShowcase = this;

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



            showcaseService.getShowcases(1, function (response) {
                $scope.banner = response.data;
                $rootScope.showcase.show();
            });


            //var getShowcases = function () {

            //    showcaseService.getShowcases(1, function (response) {

            //        vmShowcase.banner = response.data;
            //    });                
            //};


            //$scope.trustAsHtml = function () {
            //    if ($rootScope.regulations && $rootScope.regulations.length) {

            //        $scope.message = {
            //            title: $rootScope.regulations[0].name
            //        };

            //        $('.bx-scroll').jScrollPane({
            //            verticalDragMinHeight: 80,
            //            verticalDragMaxHeight: 80,
            //            horizontalDragMinWidth: 20,
            //            horizontalDragMaxWidth: 20,
            //            autoReinitialise: true
            //        });

            //        return $sce.trustAsHtml($rootScope.regulations[0].html);
            //    }
            //};

            //$scope.acceptance = function () {

            //    for (var j in $rootScope.regulations) {
            //        if ($rootScope.regulations[j].regulationTypeId === 1 || $rootScope.regulations[j].regulationTypeId === 2) {

            //            var participant = $rootScope.configuration.participant;
            //            vm.participant = {};
            //            vm.participant.name = participant.name;
            //            vm.participant.cpfCnpj = participant.cpfCnpj;
            //            vm.participant.email = participant.email;
            //            vm.participant.birthDate = participant.birthDate;
            //            vm.participant.rg = participant.rg;
            //            vm.participant.personType = participant.personType;
            //            vm.participant.genderType = participant.genderType;
            //            vm.participant.maritalStatusId = participant.maritalStatusId;
            //            vm.participant.participantStatusId = participant.participantStatusId;
            //            vm.participant.address = participant.address;
            //            vm.participant.phones = participant.phones;
            //            vm.participant.participantRegulations = [];

            //            vm.participant.participantRegulations.push({ regulationId: $rootScope.regulations[j].id });

            //            participantService.update(vm.participant)
            //                .success(function (data) {
            //                    $scope.actions.hide();
            //                });
            //        }
            //    }
            //};
        }
    }

});