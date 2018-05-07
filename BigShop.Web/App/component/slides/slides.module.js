(function () {
    angular.module('bigshop.slides', ['bigshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('slides', {
            url: '/slides',
            parent: 'base',
            templateUrl: '/app/component/slides/slideListView.html',
            controller: 'slideListController'
        }).state('add_slide', {
            url: '/add_slide',
            parent: 'base',
            templateUrl: '/app/component/slides/slideAddView.html',
            controller: 'slideAddController'
        }).state('edit_slide', {
            url: '/edit_slide/:id',
            parent: 'base',
            templateUrl: '/app/component/slides/slideEditView.html',
            controller: 'slideEditController'
        });
    }
})();