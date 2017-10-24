ticketGamesApp.service('productService', function ($http) {
    var urlBase = '/v1/product';

    var service = {
        getProduct: getProduct
    };


    function getProduct(id, successCallback, errorCallback) {

        $http.get(global.service + urlBase + '/' + id)
            .then(successCallback, errorCallback);
    }


    return service;

});