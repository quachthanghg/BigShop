(function () {
    angular.module('bigshop', ['bigshop.common', 'bigshop.products', 'bigshop.productcategories', 'bigshop.slides', 'bigshop.applicationGroups', 'bigshop.applicationUsers', 'bigshop.applicationRoles', 'bigshop.statistics', 'bigshop.postCategories', 'bigshop.posts', 'bigshop.policies', 'bigshop.sales', 'bigshop.orderdetails', 'bigshop.orders']).config(config).config(configAuthentication);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('base', {
            url: '',
            templateUrl: '/app/shared/views/baseView.html',
            abstract: true
        }).state('home', {
            url: '/admin',
            parent: 'base',
            templateUrl: '/app/component/home/homeView.html',
            controller: 'homeController'
        }).state('login', {
            url: '/login',
            templateUrl: '/app/component/login/loginView.html',
            controller: 'loginController'
        });;
        $urlRouterProvider.otherwise('/login');
    }
    function configAuthentication($httpProvider) {
        $httpProvider.interceptors.push(function ($q, $location) {
            return {
                request: function (config) {

                    return config;
                },
                requestError: function (rejection) {

                    return $q.reject(rejection);
                },
                response: function (response) {
                    if (response.status == "401") {
                        $location.path('/login');
                    }
                    //the same response/modified/or a new one need to be returned.
                    return response;
                },
                responseError: function (rejection) {

                    if (rejection.status == "401") {
                        $location.path('/login');
                    }
                    return $q.reject(rejection);
                }
            };
        });
    }
})();