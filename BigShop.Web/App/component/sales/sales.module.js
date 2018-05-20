(function () {
    angular.module('bigshop.sales', ['bigshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('sales', {
            url: '/sales',
            parent: 'base',
            templateUrl: '/app/component/sales/saleListView.html',
            controller: 'saleListController'
        }).state('add_sale', {
            url: '/add_sale',
            parent: 'base',
            templateUrl: '/app/component/sales/saleAddView.html',
            controller: 'saleAddController'
            }).state('update_sale', {
            url: '/update_sale/:id',
            parent: 'base',
            templateUrl: '/app/component/sales/saleEditView.html',
            controller: 'saleEditController'
        });
    }
})();