ticketGamesApp.service('cookieService', function ($cookies,$cookieStore) {

    var service = {
        setItem: setItem,
        removeItem: removeItem,
        getItem: getItem,
        clear: clear
    };   

    function setItem(name, value, seconds = 7199) {
        var expireDate = new Date();
        expireDate.setSeconds(seconds);        
        $cookies.put(name, JSON.stringify(value), { 'expires': expireDate });        
    };

    function removeItem(name) {
        $cookieStore.remove(name);        
    };

    function getItem(name) {
        return $cookieStore.get(name);
    };

    function property(name, value) {
        if (value) setItem(name, value); else return getItem(name);
    };

    function clear() {
        angular.forEach($cookies.getAll(), function (v, k) {
            $cookieStore.remove(k);
        });       
    };

    return service;
});