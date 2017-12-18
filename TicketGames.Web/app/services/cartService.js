ticketGamesApp.service('cartService', function ($http) {
    var urlBase = '/v1/cart';

    var service = {
        getCart: getCart,
        addCart: addCart,
        updateCart: updateCart,
        removeCart: removeCart
    };


    function getCart(successCallback, errorCallback) {

        $http.get(global.service + urlBase)
            .then(successCallback, errorCallback);

        //return $http.get(global.service + urlBase)
        //    .success(function (data) {
        //        return data;

        //    })
        //    .error(function (error, status) {
        //    });
    };

    function addCart(productId, quantity = 1, addCart = false, successCallback, errorCallback) {
        var data = "ProductId=" + productId + "&Quantity=" + quantity;

        $http.post(global.service + urlBase + "/" + addCart, data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
            .then(function (successCallback) {
                alert('Produto adicionado com sucesso!');
            },
            errorCallback);
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

    function updateCart(cart) {
        var model = JSON.stringify(cart);

        return $http.put(global.service + urlBase + '/me', model)
            .success(function (data) {
                return data;

            }).error(function (error, status) {

            });
    };

    function removeCart(id, successCallback, errorCallback) {
        $http.delete(global.service + urlBase + '/' + id)
            .then(successCallback, errorCallback);
    }

    return service;

});