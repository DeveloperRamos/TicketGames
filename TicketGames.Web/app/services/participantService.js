ticketGamesApp.service('participantService', function ($http) {
    var urlBase = '/v1/participant';

    var service = {
        getParticipant: getParticipant,
        login: login,
        update: update,
        createParticipant: createParticipant,
        getParticipantBySession: getParticipantBySession
    };

    function createParticipant(participant, successCallback, errorCallback) {
        var model = JSON.stringify(participant);

        $http.post(global.service + urlBase, participant)
            .then(successCallback, errorCallback);

    };

    function getParticipantBySession(session, successCallback, errorCallback) {
        $http.get(global.service + urlBase + '/session/' + session)
            .then(successCallback, errorCallback);
    };

    function getParticipant(participant) {
        var model = JSON.stringify(participant);

        return $http.post(global.service + urlBase + '/authenticate', model)
            .success(function (data) {
                return data;

            })
            .error(function (error, status) {
            });
    };

    function login(participant) {
        var model = JSON.stringify(participant);

        return $http.post(global.service + urlBase + '/authenticate', model)
            .success(function (data) {
                return data;

            })
            .error(function (error, status) {
            });
    };

    function update(dataModel) {
        var model = JSON.stringify(dataModel);

        return $http.put(global.service + urlBase + '/me', model)
            .error(function (error, status) {
                common.errorMessage(error, null, status);
            });
    }


    return service;

});