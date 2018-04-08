(function () {
    angular.module('bigshop.productcategories', ['bigshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('product_categories', {
            url: '/product_categories',
            templateUrl: '/app/component/productcategories/productCategoryListView.html',
            controller: 'productCategoryListController'
        }).state('add_product_category', {
            url: '/add_product_category',
            templateUrl: '/app/component/productcategories/productCategoryAddView.html',
            controller: 'productCategoryAddController'
        }).state('update_product_category', {
            url: '/update_product_category/:id',
            templateUrl: '/app/component/productcategories/productCategoryEditView.html',
            controller: 'productCategoryEditController'
        });
    }
})();