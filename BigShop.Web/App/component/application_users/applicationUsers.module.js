(function () {
    angular.module('bigshop.applicationUsers', ['bigshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('applicationUsers', {
            url: '/applicationUsers',
            parent: 'base',
            templateUrl: '/app/component/application_users/applicationUserListView.html',
            controller: 'applicationUserListController'
        }).state('add_applicationUser', {
            url: '/add_applicationUser',
            parent: 'base',
            templateUrl: '/app/component/application_users/applicationUserAddView.html',
            controller: 'applicationUserAddController'
        }).state('edit_applicationUser', {
            url: '/edit_applicationUser/:id',
            parent: 'base',
            templateUrl: '/app/component/application_users/applicationUserEditView.html',
            controller: 'applicationUserEditController'
        });
    }
})();