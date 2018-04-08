(function (app) {
    app.controller('productCategoryListController', productCategoryListController);

    productCategoryListController.$inject = ['$scope', 'apiService'];

    function productCategoryListController($scope, apiService) {
        // GetAll
        $scope.lstProductCategory = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getListProductCategory = getListProductCategory;
        function getListProductCategory(page) {
            page = page || 0;
            var config = {
                params: {
                    page: page,
                    pageSize: 2
                }
            };
            apiService.get('/Api/ProductCategory/GetAll', config, function (result) {
                $scope.lstProductCategory = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pageCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.TotalPages = Math.ceil($scope.totalCount / $scope.pageCount);
            }, function () {
                console.log("Error !");
            });
        }
        $scope.getListProductCategory();
        // End GetAll
    }
})(angular.module('bigshop.productcategories'));