'use strict';

ticketGamesApp
    .controller('productController', ['$scope', '$cookieStore', '$rootScope', '$routeParams', '$sce', 'productService',
        function ($scope, $cookieStore, $rootScope, $routeParams, $sce, productService) {
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

                    getProductsRecent(vmProduct.product.Category.Id);

                });
            };

            var getProductsRecent = function (categoryId) {

                productService.getRecentProducts(categoryId, function (response) {
                    vmProduct.recent = response.data;
                });
            }

            $scope.trustAsHtml = function (html) {

                return $sce.trustAsHtml(html);

            };

            initialize();
        }]);