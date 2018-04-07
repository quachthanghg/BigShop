(function (app) {
    app.controller('productCategoryListController', productCategoryListController);

    productCategoryListController.$inject = ['$scope', 'apiService'];

    function productCategoryListController($scope, apiService) {
        $scope.lstProductCategory = [];
        $scope.getListProductCategory = getListProductCategory;
        function getListProductCategory() {
            apiService.get('/Api/ProductCategory/GetAll', null, function (result) {
                $scope.lstProductCategory = result.data;
            }, function () {
                console.log("Error !");
            });
        }
        $scope.getListProductCategory();
    }
})(angular.module('bigshop.productcategories'));