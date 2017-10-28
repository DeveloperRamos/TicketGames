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
        //.when('/Acesso', { templateUrl: 'app/Views/Login.html', controller: 'LoginController', controllerAs: 'vmLogin' })        
        .otherwise({ redirectTo: '/' });
}]);