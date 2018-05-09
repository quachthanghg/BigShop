(function () {
    angular.module('bigshop.statistics', ['bigshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('statistics', {
            url: '/statistics',
            parent: 'base',
            templateUrl: '/app/component/statistics/statisticView.html',
            controller: 'statisticController'
        });
    }
})();