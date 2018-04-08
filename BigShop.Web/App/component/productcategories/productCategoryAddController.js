(function (app) {
    app.controller('productCategoryAddController', productCategoryAddController)
    productCategoryAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService']
    function productCategoryAddController($scope, apiService, notificationService, $state, commonService) {
        // Load combobox
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true
        }
        $scope.parentCategories = [];
        function loadParentCategories() {
            apiService.get('/Api/ProductCategory/GetAllParents', null, function (result) {
                $scope.parentCategories = result.data;
            }, function () {
                console.log("Load fail !");
            });
        }
        loadParentCategories();

        // Create data
        $scope.AddProductCategory = AddProductCategory;
        function AddProductCategory() {
            apiService.post('/Api/ProductCategory/Create', $scope.productCategory, function (result) {
                notificationService.displaySuccess(result.data.Name + " đã được thêm thành công");
                $state.go('product_categories')
            }, function (error) {
                notificationService.displayError("Thêm mới không được thêm mới");
            });
        }

        // GetSEOTitle
        $scope.getSeoTitle = getSeoTitle;
        function getSeoTitle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }
    }
})(angular.module('bigshop.productcategories'));