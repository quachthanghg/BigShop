(function (app) {
    app.controller('productCategoryListController', productCategoryListController);

    productCategoryListController.$inject = ['$scope', 'apiService', 'notificationService'];

    function productCategoryListController($scope, apiService, notificationService) {
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
                if (result.data.TotalCount > 0) {
                    notificationService.displaySuccess(result.data.TotalCount + " đã được load ra");
                }
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
        // Search
        $scope.filter = '';
        function Search(filter, page) {
            page = page || 0;
            var config = {
                params: {
                    filter: $scope.filter,
                    page: page,
                    pageSize: 2
                }
            };
            apiService.get('/Api/ProductCategory/Search', config, function (result) {
                if (result.data.TotalCount > 0) {
                    notificationService.displaySuccess("Đã tìm thấy " + result.data.TotalCount + " bản ghi");
                }
                else{
                    notificationService.displayWarning("Đã tìm thấy " + result.data.TotalCount + " bản ghi");
                }
                $scope.lstProductCategory = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pageCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.TotalPages = Math.ceil($scope.totalCount / $scope.pageCount);
            }, function () {
                console.log("Error !");
            });
        }
        $scope.Search = Search;
    }
})(angular.module('bigshop.productcategories'));