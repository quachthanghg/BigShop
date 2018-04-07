(function () {
    angular.module('bigshop.products', ['bigshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('products', {
            url: '/products',
            templateUrl: '/app/component/products/productListView.html',
            controller: 'productListController'
        }).state('add_products', {
            url: '/add_products',
            templateUrl: '/app/component/products/productAddView.html',
            controller: 'productAddController'
        });
    }
})();