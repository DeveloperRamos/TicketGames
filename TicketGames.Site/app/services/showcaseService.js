ticketGamesApp.service('showcaseService', function ($http) {
    var urlBase = '/v1/showcase';

    return {

        getShowcases: function (successCallback, errorCallback) {
            $http.get(global.service + urlBase).then(successCallback, errorCallback);
        },

        //getUsuarioId: function (UsuarioId, successCallback, errorCallback) {
        //    $http.get(urlBase + '/api/Usuario/' + UsuarioId)
        //         .success(successCallback)
        //         .error(errorCallback);
        //},        

        //UsuarioLogar: function (Usuario, successCallback, errorCallback) {
        //    var params = {
        //        Login: Usuario.Login,
        //        Senha: Usuario.Senha
        //    };

        //    $http({ method: 'POST', url: urlBase + '/api/Usuario/', data: params })
        //        .success(successCallback)
        //        .error(errorCallback);
        //},

    };
});