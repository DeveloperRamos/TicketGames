'use strict';

ticketGamesApp
    .controller('productController', ['$scope', '$cookieStore', '$rootScope', '$routeParams', '$sce', 'productService', 'cartService', 'globalService', 'cookieService',
        function ($scope, $cookieStore, $rootScope, $routeParams, $sce, productService, cartService, globalService, cookieService) {
            var vmProduct = this;

            vmProduct.options = [1, 2, 3, 4, 5];

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
                    vmProduct.quantity = 1;
                    $rootScope.lucky(vmProduct.product.Id)

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
                
                var logged = cookieService.getItem('logged');

                logged = logged ? logged : false;

                if (logged) {
                    cartService.add(productId, quantity, false, function (reponse) {

                        cartService.get(function (response) {
                            if (response.data) {
                                $rootScope.sumCart = response.data.length;
                            }
                        });

                        alert('Produto adicionado com sucesso!');
                    });
                }
                else {
                    alert('Você precisa se logar!');
                }
            };

            initialize();

        }]);