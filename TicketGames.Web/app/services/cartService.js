ticketGamesApp.service('cartService', function ($http, $rootScope) {
    var urlBase = '/v1/cart';

    var service = {
        getCart: getCart,
        addCart: addCart,
        updateCart: updateCart,
        removeCart: removeCart,
        addAddress: addAddress
    };


    function getCart(successCallback, errorCallback) {

        $http.get(global.service + urlBase)
            .then(successCallback, errorCallback);
    };

    function addCart(productId, quantity = 1, addCart = false, successCallback, errorCallback) {
        var data = "ProductId=" + productId + "&Quantity=" + quantity;

        $http.post(global.service + urlBase + "/" + addCart, data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
            .then(function (successCallback) {

                getCart(function (response) {
                    if (response.data) {
                        $rootScope.sumCart = response.data.length;
                    }
                });

                alert('Produto adicionado com sucesso!');
            },
            errorCallback);
    };

    function addAddress(address, successCallback, errorCallback) {
        var data = "Street=" + address.street + "&Number=" + address.number + "&Complement=" + address.complement + "&District=" + address.district + "&City=" + address.city + "&State=" +
            address.state + "&ZipCode=" + address.zipCode + "&Reference=" + address.reference + "&Email=" + address.email + "&HomePhone=" + address.homePhone + "&CellPhone=" + address.cellPhone;

        $http.post(global.service + urlBase + "/address", data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
            .then(successCallback, errorCallback);
    }


    //function addCart(cart) {
    //    var model = JSON.stringify(cart);

    //    return $http.post(global.service + urlBase, model)
    //        .success(function (data) {
    //            return data;

    //        })
    //        .error(function (error, status) {
    //        });
    //};

    function updateCart(cart) {
        var model = JSON.stringify(cart);

        return $http.put(global.service + urlBase + '/me', model)
            .success(function (data) {
                return data;

            }).error(function (error, status) {

            });
    };

    function removeCart(productId, successCallback, errorCallback) {
        $http.delete(global.service + urlBase + '/' + productId)
            .then(successCallback, errorCallback);
    }

    return service;

});