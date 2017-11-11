ticketGamesApp.config(['$compileProvider', '$routeProvider', '$locationProvider', function ($compileProvider, $routeProvider, $locationProvider) {
    $compileProvider.debugInfoEnabled(true);

    $locationProvider.hashPrefix('');
    $locationProvider.html5Mode({
        enabled: false,
        requireBase: true
    });

    $routeProvider
        .when('/', { templateUrl: 'app/views/home.html', controller: 'homeController', controllerAs: 'vmHome' })
        .when('/Produto/:id', { templateUrl: 'app/views/product.html', controller: 'productController', controllerAs: 'vmProduct' })
        .when('/Busca', { templateUrl: 'app/views/search.html', controller: 'searchController', controllerAs: 'vmSearch' })
        .when('/Carrinho', { templateUrl: 'app/views/cart.html', controller: 'cartController', controllerAs: 'vmCart' })
        .when('/Endereco', { templateUrl: 'app/views/address.html', controller: 'addressController', controllerAs: 'vmAddress' })
        .when('/Cadastro', { templateUrl: 'app/views/register.html', controller: 'registerController', controllerAs: 'vmRegister' })
        .when('/Pagamento', { templateUrl: 'app/views/payment.html', controller:'paymentController', controllerAs: 'vmPayment'})
        .otherwise({ redirectTo: '/' });
}]);