/// <reference path="../scripts/plugins/angular/angular.js" />

(function () {
    angular.module('bishop', ['bigshop.common', 'bigshop.products']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('home', {
            url: '/admin',
            templateUrl: '/app/component/home/homeView.html',
            controller: 'homeController'
        });
        $urlRouterProvider.otherwise('/admin');
    }

})();