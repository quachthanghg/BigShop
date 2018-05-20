(function () {
    angular.module('bigshop.policies', ['bigshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('policies', {
            url: '/policies',
            parent: 'base',
            templateUrl: '/app/component/policies/policyListView.html',
            controller: 'policyListController'
        }).state('add_policy', {
            url: '/add_policy',
            parent: 'base',
            templateUrl: '/app/component/policies/policyAddView.html',
            controller: 'policyAddController'
        }).state('update_policy', {
            url: '/update_policy/:id',
            parent: 'base',
            templateUrl: '/app/component/policies/policyEditView.html',
            controller: 'policyEditController'
        });
    }
})();