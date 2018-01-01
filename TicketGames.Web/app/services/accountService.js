ticketGamesApp.service('accountService', function ($http) {
    var urlBase = '/v1/account';

    var service = {
        get: get
    };

    function get(successCallback, errorCallback) {

        $http.get(global.service + urlBase + '/me')
            .then(successCallback, errorCallback);
    };    

    return service;

});