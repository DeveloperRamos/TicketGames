ticketGamesApp.service('productService', function ($http, $q) {
    var urlBase = '/v1/product';

    var service = {
        getProduct: getProduct,
        getRecentProducts: getRecentProducts,
        getRaffle: getRaffle,
        getValue: getValue
    };


    function getProduct(id, successCallback, errorCallback) {

        $http.get(global.service + urlBase + '/' + id)
            .then(successCallback, errorCallback);
    };

    function getRecentProducts(categoryId, successCallback, errorCallback) {
        $http.get(global.service + urlBase + '/recent/' + categoryId)
            .then(successCallback, errorCallback);
    };

    function getRaffle(productId, successCallback, errorCallback) {

        $http.get(global.service + urlBase + '/raffle/' + productId)
            .then(successCallback, errorCallback);
    };

    function getValue(productId, successCallback, errorCallback) {
        $http.get(global.service + urlBase + '/value/' + productId)
            .then(successCallback, errorCallback);
    };

    return service;

});