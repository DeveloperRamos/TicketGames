'use strict';

ticketGamesApp
    .controller('productController', ['$scope', '$cookieStore', '$rootScope', '$routeParams', '$sce', 'productService', 'cartService', 'globalService',
        function ($scope, $cookieStore, $rootScope, $routeParams, $sce, productService, cartService, globalService) {
            var vmProduct = this;

            var initialize = function () {
                getProduct();
            };


            var getProduct = function () {

                var productId = $routeParams.id;

                productService.getProduct(productId, function (response) {

                    if ($rootScope.bread) {
                        $rootScope.bread.show();
                        $rootScope.showcase.hide();

                        var obj = {
                            "title": response.data.Name,
                            "pages": [{
                                "page": "Busca?cat=" + response.data.Category.Id,
                                "title": response.data.Category.Name
                            }]
                        };

                        $rootScope.bread.text(obj);
                    }

                    vmProduct.product = response.data;
                    vmProduct.quantity = [];

                    //productService.getValue(productId, function (value) {                       

                    //    vmProduct.product.Value = value.data;
                    //});

                    getProductsRecent(vmProduct.product.Category.Id);

                });
            };

            var getProductsRecent = function (categoryId) {

                productService.getRecentProducts(categoryId, function (response) {
                    vmProduct.recent = response.data;
                });
            };

            var value = function (productId) {

                productService.getValue(productId, function (response) {

                    return response.data;

                });
            };


            $scope.trustAsHtml = function (html) {

                return $sce.trustAsHtml(html);

            };

            vmProduct.addCart = function (productId, quantity) {

                var logged = globalService.getItem('logged');

                logged = logged ? logged : false;

                if (logged) {
                    cartService.addCart(productId,quantity);
                }
                else {
                    alert('Você precisa se logar!');
                }
            };

            initialize();

        }]);