ticketGamesApp.service('cookieService', function ($cookies,$cookieStore) {

    var service = {
        setItem: setItem,
        removeItem: removeItem,
        getItem: getItem,
        clear: clear
    };   

    function setItem(name, value) {
        $cookieStore.put(name, value);        
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
        angular.forEach($cookies, function (v, k) {
            $cookieStore.remove(k);
        });       
    };

    return service;
});