(function () {
    angular.module('bigshop.orders', ['bigshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('orders', {
            url: '/orders',
            parent: 'base',
            templateUrl: '/app/component/orders/orderListView.html',
            controller: 'orderListController'
        })
    }
})();