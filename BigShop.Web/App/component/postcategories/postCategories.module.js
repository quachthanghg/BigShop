(function () {
    angular.module('bigshop.postCategories', ['bigshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('postCategories', {
            url: '/postCategories',
            parent: 'base',
            templateUrl: '/app/component/postCategories/postCategoryListView.html',
            controller: 'postCategoryListController'
        }).state('add_postCategory', {
            url: '/add_postCategory',
            parent: 'base',
            templateUrl: '/app/component/postCategories/postCategoryAddView.html',
            controller: 'postCategoryAddController'
            }).state('edit_postCategory', {
                url: '/edit_postCategory/:id',
            parent: 'base',
            templateUrl: '/app/component/postCategories/postCategoryEditView.html',
            controller: 'postCategoryEditController'
        });
    }
})();