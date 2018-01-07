'use strict';

ticketGamesApp
    .controller('paymentController', ['$scope', '$cookieStore', '$rootScope', '$routeParams', '$sce', '$location', 'cartService', 'cookieService', 'accountService', 'globalService', 'participantService',
        function ($scope, $cookieStore, $rootScope, $routeParams, $sce, $location, cartService, cookieService, accountService, globalService, participantService) {
            var vmPayment = this;

            vmPayment.bandImages = [];
            vmPayment.band = [];
            vmPayment.Total = 0;
            vmPayment.totalPoints = 0;
            vmPayment.totalMoney = 0;
            vmPayment.installment = 1;
            vmPayment.enableBillet = false;
            vmPayment.enableCredit = false;
            vmPayment.enablePoint = false;
            vmPayment.enableParcel = false;
            vmPayment.brand = '';


            vmPayment.expiryMonth = [
                { dig: 1, description: 'Janeiro' },
                { dig: 2, description: 'Fevereiro' },
                { dig: 3, description: 'Março' },
                { dig: 4, description: 'Abril' },
                { dig: 5, description: 'Maio' },
                { dig: 6, description: 'Junho' },
                { dig: 7, description: 'Julho' },
                { dig: 8, description: 'Agosto' },
                { dig: 9, description: 'Setembro' },
                { dig: 10, description: 'Outubro' },
                { dig: 11, description: 'Novembro' },
                { dig: 12, description: 'Dezembro' }]


            vmPayment.expiryYear = [
                { dig: 18, description: '2018' },
                { dig: 19, description: '2019' },
                { dig: 20, description: '2020' },
                { dig: 21, description: '2021' },
                { dig: 22, description: '2022' },
                { dig: 23, description: '2023' },
                { dig: 24, description: '2024' },
                { dig: 25, description: '2025' }]




            $rootScope.credit = {
                show: function () {

                    if ($(".credit").is(":visible"))
                        return true;

                    $(".credit").show();
                    return false;
                },

                hide: function () {
                    $(".credit").hide();
                },
            };


            vmPayment.balance = parseFloat(globalService.getItem('balance'));

            var initialize = function () {
                //if (!$rootScope.cart)
                //    $location.path('/Carrinho');
                getCart();

                if ($rootScope.bread) {
                    $rootScope.bread.show();
                    $rootScope.showcase.hide();

                    var obj = {
                        "title": 'Pagamento',
                        "fa": 'fa fa-money',
                        "pages": [{
                            "page": "Carrinho",
                            "title": "Carrinho",
                            "fa": "fa fa-shopping-cart"
                        },
                        {
                            "page": "Endereco",
                            "title": "Endereço",
                            "fa": "fa fa-bars"
                        }]
                    };

                    $rootScope.bread.text(obj);
                }
            };

            var getInstallments = function (brand) {

                PagSeguroDirectPayment.getInstallments({
                    amount: vmPayment.totalMoney,
                    brand: brand,
                    maxInstallmentNoInterest: 5,
                    success: function (response) {
                        //opções de parcelamento disponível
                        var result = response.installments;


                        var log = [];
                        angular.forEach(result, function (value, key) {

                            //vmPayment.plots = value;

                            var sum = 0;
                            vmPayment.plots = [];

                            angular.forEach(value, function (value, key) {

                                sum = sum + 1;

                                vmPayment.plots.push({
                                    parcel: sum,
                                    description: value.quantity + ' x ' + "R$ " + value.installmentAmount.toFixed(2).replace(".", ",")
                                });
                            }, log);

                        }, log);

                        vmPayment.enableParcel = true;
                    },
                    error: function (response) {
                        //tratamento do erro
                    },
                    complete: function (response) {
                        //tratamento comum para todas chamadas
                    }
                });
            };

            var pagSeguro = function (value) {

                participantService.getSession(function (response) {

                    var session = response.data;

                    PagSeguroDirectPayment.setSessionId(session);

                    PagSeguroDirectPayment.getPaymentMethods({
                        amount: value,
                        success: function (response) {
                            //meios de pagamento disponíveis
                            var payments = response.paymentMethods;

                            var credits = response.paymentMethods.CREDIT_CARD.options;

                            var log = [];
                            angular.forEach(credits, function (value, key) {

                                if (value.status === 'AVAILABLE' && (value.code === 101 || value.code === 102 || value.code === 103 || value.code === 105 || value.code === 107)) {

                                    vmPayment.bandImages.push({
                                        band: value.name,
                                        url: 'https://stc.pagseguro.uol.com.br' + value.images.MEDIUM.path
                                    });

                                    vmPayment.band.push({
                                        code: value.code,
                                        band: value.name
                                    })
                                }
                            }, log);

                            vmPayment.billet = {
                                code: response.paymentMethods.BOLETO.options.BOLETO.code,
                                name: response.paymentMethods.BOLETO.options.BOLETO.name
                            }

                        },
                        error: function (response) {
                            //tratamento do erro
                        },
                        complete: function (response) {
                            //tratamento comum para todas chamadas
                        }
                    });
                });
            };

            var getAddress = function () {

                if (!$rootScope.cartId) {
                    $location.path('/Carrinho');
                } else {
                    cartService.getAddress($rootScope.cartId, function (response) {
                        vmAddress.address = response.data;
                    });
                }
            };

            var getCart = function () {

                var logged = cookieService.getItem('logged');

                logged = logged ? logged : false;

                if (!logged) {
                    $location.path('/');
                }

                cartService.get(function (response) {

                    vmPayment.carts = response.data;

                    if (response.data) {
                        var log = [];
                        var cartId;
                        angular.forEach(response.data, function (value, key) {
                            cartId = value.CartId;
                            vmPayment.Total += (value.Price * value.Quantity);

                        }, log);

                        if (vmPayment.Total > vmPayment.balance) {
                            vmPayment.totalPoints = vmPayment.balance;
                            vmPayment.totalMoney = vmPayment.Total - vmPayment.balance;
                            pagSeguro(vmPayment.totalMoney);
                        }
                        else {
                            vmPayment.enablePoint = true;
                            vmPayment.totalPoints = vmPayment.Total;
                        }

                        cartService.getAddress(cartId, function (response) {
                            vmPayment.address = response.data;
                        });


                    }

                }, function (error) {

                    $location.path('/');
                    var teste = error;
                });
            };


            vmPayment.enable = function (payment) {

                switch (payment) {
                    case 'billet': {

                        vmPayment.enableBillet = true;
                        vmPayment.enableCredit = false;
                        vmPayment.enablePoint = false;

                        break;
                    }
                    case 'credit': {

                        vmPayment.enableBillet = false;
                        vmPayment.enableCredit = true;
                        vmPayment.enablePoint = false;
                        break;
                    }
                    default: {
                        vmPayment.enableBillet = false;
                        vmPayment.enableCredit = false;
                    }
                }
            };


            vmPayment.getBrandAndParcel = function (card) {

                if (card.length === 16) {

                    PagSeguroDirectPayment.getBrand({
                        cardBin: card,
                        success: function (response) {
                            //bandeira encontrada

                            var brand = response.brand.name;

                            vmPayment.brand = brand;

                            var band = angular.element(document.querySelector('#' + brand.toUpperCase()));
                            band.removeClass('creditHidden');
                            band.addClass('creditNotHidden');

                            getInstallments(brand);

                        },
                        error: function (response) {
                            //tratamento do erro
                            alert('Cartão está invalido');
                        },
                        complete: function (response) {
                            //tratamento comum para todas chamadas
                        }
                    });


                }
            };

            vmPayment.redemption = function (order) {

                var fincla = order;


            };

            $scope.trustAsHtml = function (html) {

                return $sce.trustAsHtml(html);

            };

            initialize();
        }]);