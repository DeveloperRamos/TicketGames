ticketGamesApp.config(['$compileProvider', '$routeProvider', function ($compileProvider, $routeProvider) {
    $compileProvider.debugInfoEnabled(false);

    $routeProvider
        .when('/', { templateUrl: 'app/views/home.html', controller: 'homeController', controllerAs: 'vmHome' })
        //.when('/Acesso', { templateUrl: 'app/Views/Login.html', controller: 'LoginController', controllerAs: 'vmLogin' })        
        .otherwise({ redirectTo: '/' });


}]);