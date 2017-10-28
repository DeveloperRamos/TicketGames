ticketGamesApp.service('searchService', function ($http) {
    var urlBase = '/v1/search';

    var service = {
        getCategories: getCategories
    };

    function getCategories(successCallback, errorCallback) {

        $http.get(global.service + urlBase)
            .then(successCallback, errorCallback);
    }


    return service;

});