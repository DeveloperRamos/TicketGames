ticketGamesApp.service('searchService', function ($http) {
    var urlBase = '/v1/product/search';

    var service = {
        getCategories: getCategories,
        searchCep: searchCep,
        searchProducts: searchProducts
    };

    function getCategories(successCallback, errorCallback) {

        $http.get(global.service + urlBase)
            .then(successCallback, errorCallback);
    };

    function searchCep(cep, successCallback, errorCallback) {
        $http.get('//viacep.com.br/ws/' + cep + '/json/')
            .then(successCallback, errorCallback);
    };


    function searchProducts(search, successCallback, errorCallback) {
        var model = JSON.stringify(search);
        

        $http.post(global.service + urlBase, search)
            .then(successCallback, errorCallback);

    };


    return service;

});