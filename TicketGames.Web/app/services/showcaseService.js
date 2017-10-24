ticketGamesApp.service('showcaseService', function ($http) {
    var urlBase = '/v1/showcase';

    var service = {
        getShowcases: getShowcases
    };


    function getShowcases(identifier, successCallback, errorCallback) {

        $http.get(global.service + urlBase + '/' + identifier)
            .then(successCallback, errorCallback);
    }


    return service;

});