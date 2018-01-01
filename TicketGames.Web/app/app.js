var ticketGamesApp = angular.module('ticketGamesApp', [
    'ngRoute',
    'ngCookies',
    'ngAnimate'
    //'ngMask',    
    //'ngSanitize'
]);

// Handle routing errors and success events
ticketGamesApp.run(['$rootScope', '$route', '$location', '$cookieStore', '$templateCache', 'globalService', '$q', '$routeParams', '$document', 'cartService', 'accountService',
    function ($rootScope, $route, $location, $cookieStore, $templateCache, globalService, $q, $routeParams, $document, cartService, accountService) {

        $rootScope.$on("$locationChangeStart", function (event, next, current) {

            $rootScope.sumCart = 0;
            $rootScope.balance = 0;

            var logged = globalService.getItem('logged');

            logged = logged ? true : false;

            // $rootScope.globalService = globalService;
            //$rootScope.configuration = GlobalService.configuration();


            //if ($rootScope.configuration && $rootScope.configuration.Client) {
            //    $rootScope.Logged = true;
            //}
            //else {
            //    $rootScope.Logged = false;
            //}


            if (logged) {
                cartService.get(function (response) {
                    if (response.data) {
                        $rootScope.sumCart = response.data.length;
                    }
                });                
            }
            else {
                $rootScope.sumCart = 0;
            }


            if ($rootScope.bread && $rootScope.showcase) {
                if ($location.url() === "" || $location.url() === "/") {
                    $rootScope.bread.hide();
                    $rootScope.showcase.show();
                }
                else {
                    $rootScope.bread.show();
                    $rootScope.showcase.hide();

                }
            }
            else {
                if ($(".home-slider").is(":visible"))
                    return true;

                $(".home-slider").show();
                return false;
            }
        });

        $rootScope.$on('$routeChangeSuccess', function (e, current, pre) {

            var teste = 'marcio ramos';
            var rot = 'trocou de rota';
            e.currentScope.isPaneShown = true;



            //var gtm_obj = $rootScope.getGTMObj('page', $location.path());
            //var gtm_settings = $route.current.$$route.settings ? $route.current.$$route.settings.gtm : null;
            //if (gtm_settings) {
            //    if (gtm_settings) {
            //        if (gtm_settings.event)
            //            gtm_obj.event = gtm_settings.event;
            //        gtm_obj = $rootScope.mapObj($routeParams, gtm_settings.routeParams, gtm_obj);
            //    }
            //}
            //$rootScope.sendGTMObj(gtm_obj);

            //if (!authService.ticket() && ($route.current.$$route.settings.denyAnonynous || $route.current.$$route.settings.denyAnonynous == undefined)) {
            //    if ($location.path() == '/primeiroacesso') {
            //        $rootScope.loggedUser = true;
            //        $location.path('/primeiroacesso');
            //    } else {
            //        common.logOff();
            //    }
            //}
        });

        //$(window).on('scroll', function () {
        //    mediaWidth = Math.max($(window).width(), window.innerWidth);
        //    if (mediaWidth > 999) {
        //        var currentScrollWindow = $(window).scrollTop();
        //        var currentHeight = $(window).height();

        //        if (currentScrollWindow > 80) {
        //            $('.wr-busca').addClass('fixed');
        //            if (currentScrollWindow > 647) {
        //                $('.wr-categorias').addClass('fixed');
        //            }
        //            else {
        //                $('.wr-categorias').removeClass('fixed').removeAttr('style');
        //            }
        //        }
        //        else {
        //            $('.wr-busca,.wr-categorias').removeClass('fixed').removeAttr('style');
        //        }

        //        $('.wr-busca').on('mouseenter', function () {
        //            if ($(this).hasClass('fixed') && $('.wr-categorias').hasClass('fixed'))
        //                $('.wr-categorias').stop(true, false).animate({ top: '82px' });
        //        });
        //        $('.wr-categorias').on('mouseleave', function () {
        //            if ($(this).hasClass('fixed'))
        //                $('.wr-categorias').stop(true, false).animate({ top: '1px' });
        //        });
        //    }
        //});       

    }]);

ticketGamesApp.factory('BearerAuthInterceptor', function ($window, $q, globalService) {
    return {
        request: function (config) {
            config.headers = config.headers || {};
            if (globalService.getItem('token')) {

                if (config.url.indexOf("viacep") != -1) {
                    var teste = 'tem a string';
                }
                else {
                    // may also use sessionStorage
                    config.headers.Authorization = 'Bearer ' + globalService.getItem('token');
                }
            }
            return config || $q.when(config);
        },
        response: function (response) {
            if (response.status === 401) {
                //  Redirect user to login page / signup Page.
            }
            return response || $q.when(response);
        }
    };
});

// Register the previously created AuthInterceptor.
ticketGamesApp.config(function ($httpProvider) {
    $httpProvider.interceptors.push('BearerAuthInterceptor');
});