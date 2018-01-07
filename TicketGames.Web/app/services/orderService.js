ticketGamesApp.service('orderService', function ($http, $rootScope) {
    var urlBase = '/v1/order';

    var service = {
        redemption: redemption
    };

    function redemption(order, successCallback, errorCallback) {
        $http.post(global.service + urlBase, order)
            .then(successCallback, errorCallback);
    }

    return service;

});