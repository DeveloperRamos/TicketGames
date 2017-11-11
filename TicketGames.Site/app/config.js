ticketGamesApp.config(['$stateProvider', '$urlRouterProvider', '$ocLazyLoadProvider', 'IdleProvider', 'KeepaliveProvider',
    function ($stateProvider, $urlRouterProvider, $ocLazyLoadProvider, IdleProvider, KeepaliveProvider) {
        // Configure Idle settings
        IdleProvider.idle(5); // in seconds
        IdleProvider.timeout(120); // in seconds

        $urlRouterProvider.otherwise("/");

        $ocLazyLoadProvider.config({
            // Set to true if you want to see what and when is dynamically loaded
            debug: false
        });



        $stateProvider

            .state('/', {
                abstract: true,
                url: "/",
                templateUrl: "views/common/content.html",
                resolve: {
                    loadPlugin: function ($ocLazyLoad) {
                        return $ocLazyLoad.load([
                            {
                                files: ['Scripts/plugins/footable/footable.all.min.js', 'css/plugins/footable/footable.core.css']
                            },
                            {
                                name: 'ui.footable',
                                files: ['Scripts/plugins/footable/angular-footable.js']
                            }
                        ]);
                    }
                }
            })
            .state('/', {
                url: "/",
                templateUrl: "views/home.html",
                data: { pageTitle: 'E-commerce grid' },
                controller:"homeController"
            });
    }]);