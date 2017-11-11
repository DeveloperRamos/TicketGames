ticketGamesApp.service('searchService', function ($http) {
    var urlBase = '/v1/search';

    var service = {
        getCategories: getCategories,
        searchCep: searchCep
    };

    function getCategories(successCallback, errorCallback) {

        $http.get(global.service + urlBase)
            .then(successCallback, errorCallback);
    };

    function searchCep(cep, successCallback, errorCallback) {
        $http.get('//viacep.com.br/ws/' + cep + '/json/')
            .then(successCallback, errorCallback);
    };


    return service;

});