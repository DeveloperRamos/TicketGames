ticketGamesApp.service('orderService', function ($http, $rootScope) {
    var urlBase = '/v1/order';

    var service = {
        redemption: redemption,
        getInstallments: getInstallments
    };

    function redemption(order, successCallback, errorCallback) {
        $http.post(global.service + urlBase, order)
            .then(successCallback, errorCallback);
    };

    function getInstallments(brand, successCallback, errorCallback) {
        $http.get(global.service + urlBase + '/installment/' + brand)
            .then(successCallback, errorCallback);
    }

    return service;

});