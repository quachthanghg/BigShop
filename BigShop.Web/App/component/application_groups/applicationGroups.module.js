(function () {
    angular.module('bigshop.applicationGroups', ['bigshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('applicationGroups', {
            url: '/applicationGroups',
            parent: 'base',
            templateUrl: '/app/component/application_groups/applicationGroupListView.html',
            controller: 'applicationGroupListController'
        }).state('add_applicationGroup', {
            url: '/add_applicationGroup',
            parent: 'base',
            templateUrl: '/app/component/application_groups/applicationGroupAddView.html',
            controller: 'applicationGroupAddController'
            }).state('edit_applicationGroup', {
            url: '/edit_applicationGroup/:id',
            parent: 'base',
            templateUrl: '/app/component/application_groups/applicationGroupEditView.html',
            controller: 'applicationGroupEditController'
        });
    }
})();