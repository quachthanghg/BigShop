(function () {
    angular.module('bigshop.posts', ['bigshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('posts', {
            url: '/posts',
            parent: 'base',
            templateUrl: '/app/component/posts/postListView.html',
            controller: 'postListController'
        }).state('add_post', {
            url: '/add_post',
            parent: 'base',
            templateUrl: '/app/component/posts/postAddView.html',
            controller: 'postAddController'
        }).state('edit_post', {
            url: '/edit_post/:id',
            parent: 'base',
            templateUrl: '/app/component/posts/postEditView.html',
            controller: 'postEditController'
        });
    }
})();