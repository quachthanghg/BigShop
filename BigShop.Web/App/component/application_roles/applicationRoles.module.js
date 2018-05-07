(function () {
    angular.module('bigshop.applicationRoles', ['bigshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('applicationRoles', {
            url: '/applicationRoles',
            parent: 'base',
            templateUrl: '/app/component/application_roles/applicationRoleListView.html',
            controller: 'applicationRoleListController'
        }).state('add_applicationRole', {
            url: '/add_applicationRole',
            parent: 'base',
            templateUrl: '/app/component/application_roles/applicationRoleAddView.html',
            controller: 'applicationRoleAddController'
            }).state('edit_applicationRole', {
            url: '/edit_applicationRole/:id',
            parent: 'base',
            templateUrl: '/app/component/application_roles/applicationRoleEditView.html',
            controller: 'applicationRoleEditController'
        });
    }
})();