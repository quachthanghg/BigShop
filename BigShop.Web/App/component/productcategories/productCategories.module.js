(function () {
    angular.module('bigshop.productcategories', ['bigshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('product_categories', {
            url: '/product_categories',
            templateUrl: '/app/component/productcategories/productCategoryListView.html',
            controller: 'productCategoryListController'
        })
    }
})();