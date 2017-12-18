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

    function getParticipant(successCallback, errorCallback) {        
        $http.get(global.service + urlBase + '/me')
            .then(successCallback, errorCallback);
    };

    function login(participant, successCallback, errorCallback) {
        var data = "grant_type=password&username=" + participant.login + "&password=" + participant.password;                   

        $http.post(global.service + '/token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
            .then(successCallback, errorCallback);
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