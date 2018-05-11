(function () {
    angular.module('bigshop.postcategories', ['bigshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('postcategories', {
            url: '/postcategories',
            parent: 'base',
            templateUrl: '/app/component/posts/postCategoryListView.html',
            controller: 'postCategoryListController'
        }).state('add_postcategory', {
            url: '/add_postcategory',
            parent: 'base',
            templateUrl: '/app/component/posts/postCategoryAddView.html',
            controller: 'postCategoryAddController'
            }).state('edit_postcategory', {
                url: '/edit_postcategory/:id',
            parent: 'base',
            templateUrl: '/app/component/posts/postCategoryEditView.html',
            controller: 'postCategoryEditController'
        });
    }
})();