﻿var ticketGamesApp = angular.module('ticketGamesApp', [
    'ngRoute',
    'ngCookies'
    //'ngMask',
    //'ngAnimate',
    //'ngSanitize'
]);

// Handle routing errors and success events
ticketGamesApp.run(['$rootScope', '$route', '$location', '$cookieStore', '$templateCache', 'globalService', '$q', '$routeParams', '$document',
    function ($rootScope, $route, $location, $cookieStore, $templateCache, globalService, $q, $routeParams, $document) {

        $rootScope.$on("$locationChangeStart", function (event, next, current) {

            // $rootScope.globalService = globalService;
            //$rootScope.configuration = GlobalService.configuration();


            //if ($rootScope.configuration && $rootScope.configuration.Client) {
            //    $rootScope.Logged = true;
            //}
            //else {
            //    $rootScope.Logged = false;
            //}
        });

        $rootScope.$on('$routeChangeSuccess', function (e, current, pre) {

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