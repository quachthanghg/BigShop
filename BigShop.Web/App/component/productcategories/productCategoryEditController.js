(function (app) {
    app.controller('productCategoryEditController', productCategoryEditController)
    productCategoryEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams','commonService']
    function productCategoryEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
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

        // Update data
        $scope.UpdateProductCategory = UpdateProductCategory;
        function UpdateProductCategory() {
            apiService.put('/Api/ProductCategory/Update', $scope.productCategory, function (result) {
                notificationService.displaySuccess(result.data.Name + " đã được cập nhật");
                $state.go('product_categories')
            }, function (error) {
                notificationService.displayError("Cập nhật không được thành công !");
            });
        }

        // Load detail
        $scope.DetailProductCategory = DetailProductCategory;
        function DetailProductCategory() {
            apiService.get('/Api/ProductCategory/GetById/' + $stateParams.id, null, function (result) {
                $scope.productCategory = result.data;
            }, function (error) {
                notificationService.displayError("Không lấy được dữ liệu !");
            });
        }
        DetailProductCategory();
        
        // GetSEOTitle
        $scope.getSeoTitle = getSeoTitle;
        function getSeoTitle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }
    }
})(angular.module('bigshop.productcategories'));