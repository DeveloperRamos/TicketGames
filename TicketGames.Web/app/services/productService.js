ticketGamesApp.service('productService', function ($http) {
    var urlBase = '/v1/product';

    var service = {
        getProduct: getProduct,
        getRecentProducts: getRecentProducts
    };


    function getProduct(id, successCallback, errorCallback) {

        $http.get(global.service + urlBase + '/' + id)
            .then(successCallback, errorCallback);
    };

    function getRecentProducts(categoryId, successCallback, errorCallback) {
        $http.get(global.service + urlBase + '/recent/' + categoryId)
            .then(successCallback, errorCallback);
    }

    return service;

});