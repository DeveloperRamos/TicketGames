ticketGamesApp.directive('showcase', function () {
    return {
        restrict: "EA",
        scope: {
            message: "@"
            //actions: "="
        },
        replace: false,
        templateUrl: "app/directives/showcase/showcase.html",
        controller: function ($scope) {
            var vm = this;

            //$scope.actions = {
            //    show: function () {

            //        if ($(".overlay, .bx-modal").is(":visible"))
            //            return true;

            //        $(".overlay, .bx-modal").show();
            //        return false;
            //    },

            //    hide: function () {
            //        $(".overlay, .bx-modal").hide();
            //    },
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