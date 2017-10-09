ticketGamesApp.service('globalService', function ($rootScope) {

    var service = {
        setItem: setItem,
        removeItem: removeItem,
        getItem: getItem,
        clear: clear,
        configuration: configuration,
        participant: participant
    };
    function configuration(obj) {
        if (obj) {
            setItem('configuration', JSON.stringify(obj));
            $rootScope.configuration = JSON.stringify(obj);
        } else {
            var data = JSON.parse(getItem('configuration'));
            return data;
        }
    };

    function participant(obj) {
        var _configuration = configuration() || {};
        if (obj) {
            //obj.name = obj.name.trim();
        }
        _configuration.participant = obj;
        configuration(_configuration);
    };

    function setItem(name, value) {
        localStorage.setItem(name, value);
    };

    function removeItem(name) {
        localStorage.removeItem(name);
    };

    function getItem(name) {
        return localStorage.getItem(name);
    };

    function property(name, value) {
        if (value) setItem(name, value); else return getItem(name);
    };

    function clear() {
        localStorage.clear();
    };

    return service;
});