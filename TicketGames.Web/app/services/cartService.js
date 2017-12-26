ticketGamesApp.service('cartService', function ($http, $rootScope) {
    var urlBase = '/v1/cart';

    var service = {
        get: get,
        add: add,
        update: update,
        remove: remove,
        addAddress: addAddress,
        getAddress: getAddress
    };


    function get(successCallback, errorCallback) {
        $http.get(global.service + urlBase)
            .then(successCallback, errorCallback);
    };

    function add(productId, quantity = 1, addCart = false, successCallback, errorCallback) {
        var data = "ProductId=" + productId + "&Quantity=" + quantity;

        $http.post(global.service + urlBase + "/" + addCart, data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
            .then(successCallback,errorCallback);
    };

    function addAddress(address, successCallback, errorCallback) {
        var data = "Name=" + address.Name + "&Street=" + address.Street + "&Number=" + address.Number + "&Complement=" + address.Complement + "&District=" + address.District + "&City=" + address.City + "&State=" +
            address.State + "&ZipCode=" + address.ZipCode + "&Reference=" + address.Reference + "&Email=" + address.Email + "&HomePhone=" + address.HomePhone + "&CellPhone=" + address.CellPhone;

        $http.post(global.service + urlBase + "/address", data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
            .then(successCallback, errorCallback);
    };

    function getAddress(cartId, successCallback, errorCallback) {
        $http.get(global.service + urlBase + "/address/" + cartId)
            .then(successCallback, errorCallback);
    };


    //function addCart(cart) {
    //    var model = JSON.stringify(cart);

    //    return $http.post(global.service + urlBase, model)
    //        .success(function (data) {
    //            return data;

    //        })
    //        .error(function (error, status) {
    //        });
    //};

    function update(productId, quantity, successCallback, errorCallback) {
        var data = "ProductId=" + productId + "&Quantity=" + quantity;

        $http.put(global.service + urlBase, data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
            .then(successCallback,errorCallback);
    };

    function remove(productId, successCallback, errorCallback) {
        $http.delete(global.service + urlBase + '/' + productId)
            .then(successCallback, errorCallback);
    }

    return service;

});