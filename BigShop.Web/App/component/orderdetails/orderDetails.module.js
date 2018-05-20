(function () {
    angular.module('bigshop.orderdetails', ['bigshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('orderdetails', {
            url: '/orderdetails',
            parent: 'base',
            templateUrl: '/app/component/orderdetails/orderdetailListView.html',
            controller: 'orderDetailListController'
        }).state('add_orderdetail', {
            url: '/add_orderdetail',
            parent: 'base',
            templateUrl: '/app/component/orderdetails/orderdetailAddView.html',
            controller: 'orderDetailAddController'
        }).state('edit_orderdetail', {
            url: '/edit_orderdetail/:id',  
            parent: 'base',
            templateUrl: '/app/component/orderdetails/orderdetailEditView.html',
            controller: 'orderDetailEditController'
        });
    }
})();