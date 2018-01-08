'use strict';

ticketGamesApp
    .controller('paymentController',
    ['$scope', '$cookieStore', '$rootScope', '$routeParams', '$sce', '$location', 'cartService', 'cookieService', 'accountService', 'globalService', 'participantService', 'searchService', 'orderService',
        function ($scope, $cookieStore, $rootScope, $routeParams, $sce, $location, cartService, cookieService, accountService, globalService, participantService, searchService, orderService) {
            var vmPayment = this;

            var startObjects = function () {

                vmPayment.order = {};

                vmPayment.balance = parseFloat(globalService.getItem('balance'));

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
                vmPayment.order.paymentType = 1;
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
            };

            startObjects();

            var initialize = function () {

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
                    maxInstallmentNoInterest: 12,
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
                                    quantity: sum,
                                    description: value.quantity + ' x ' + "R$ " + value.installmentAmount.toFixed(2).replace(".", ","),
                                    value: value.installmentAmount.toFixed(2)
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

                vmPayment.order = {};

                switch (payment) {
                    case 'billet': {

                        vmPayment.enableBillet = true;
                        vmPayment.enableCredit = false;
                        vmPayment.enablePoint = false;

                        vmPayment.order.paymentType = 2;

                        break;
                    }
                    case 'credit': {

                        vmPayment.order.card = {
                            owner: true,
                            brand: '',
                            number: '',
                            expiryMonth: '',
                            expiryYear: '',
                            cvv: '',
                            parcel: {},
                            billingAddress: {},
                            creditCardHolder: {}
                        };


                        vmPayment.enableBillet = false;
                        vmPayment.enableCredit = true;
                        vmPayment.enablePoint = false;

                        vmPayment.order.paymentType = 3;

                        break;
                    }
                    default: {

                        vmPayment.enableBillet = false;
                        vmPayment.enableCredit = false;

                        vmPayment.order.paymentType = 1;
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
                            vmPayment.order.card.brand = brand;

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

                switch (order.paymentType) {
                    case 3: {

                        order.card.senderHash = PagSeguroDirectPayment.getSenderHash();

                        PagSeguroDirectPayment.createCardToken({
                            brand: order.card.brand,
                            cardNumber: order.card.number,
                            cvv: order.card.cvv,
                            expirationMonth: order.card.expiryMonth,
                            expirationYear: order.card.expiryYear,
                            success: function (response) {
                                //token gerado, esse deve ser usado na chamada da API do Checkout Transparente

                                var teste = response;
                                order.card.creditCardToken = response.card.token;

                                orderService.redemption(order, function (response) {

                                });



                            },
                            error: function (response) {
                                //tratamento do erro
                            },
                            complete: function (response) {
                                //tratamento comum para todas chamadas
                            }
                        });

                        break;
                    }
                    default: {

                    }
                };

            };

            $scope.trustAsHtml = function (html) {

                return $sce.trustAsHtml(html);

            };

            vmPayment.search = function (valor) {

                //Nova variável "cep" somente com dígitos.
                var cep = valor.replace(/\D/g, '');

                //Verifica se campo cep possui valor informado.
                if (cep !== "") {

                    //Expressão regular para validar o CEP.
                    var validacep = /^[0-9]{8}$/;

                    searchService.searchCep(cep, function (response) {

                        if (!response.data.logradouro) {
                            var street = angular.element(document.querySelector('#street'));
                            street[0].disabled = false;
                        } else {
                            vmPayment.order.card.billingAddress.Street = response.data.logradouro;
                        }

                        if (!response.data.bairro) {
                            var district = angular.element(document.querySelector('#district'));
                            district[0].disabled = false;
                        } else {
                            vmPayment.order.card.billingAddress.District = response.data.bairro;
                        }

                        vmPayment.order.card.billingAddress.City = response.data.localidade;
                        vmPayment.order.card.billingAddress.State = response.data.uf;

                    });


                } //end if.
                else {
                    //cep sem valor, limpa formulário.
                    alert("Formato de CEP inválido.");
                }
            };

            initialize();
        }]);