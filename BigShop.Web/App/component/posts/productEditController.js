(function (app) {
    app.controller('productEditController', productEditController)
    productEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams','commonService']
    function productEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
        // Load combobox
        $scope.product = {
            CreatedDate: new Date(),
            Status: true
        }
        $scope.parentCategories = [];
        function loadParentCategories() {
            apiService.get('/Api/Product/GetAllParents', null, function (result) {
                $scope.parentCategories = result.data;
            }, function () {
                console.log("Load fail !");
            });
        }
        loadParentCategories();

        // Update data
        $scope.UpdateProduct = UpdateProduct;
        function UpdateProduct() {
            apiService.put('/Api/Product/Update', $scope.product, function (result) {
                notificationService.displaySuccess(result.data.Name + " đã được cập nhật");
                $state.go('products')
            }, function (error) {
                notificationService.displayError("Cập nhật không được thành công !");
            });
        }

        // Load detail
        $scope.DetailProduct = DetailProduct;
        function DetailProduct() {
            apiService.get('/Api/Product/GetById/' + $stateParams.id, null, function (result) {
                $scope.product = result.data;
                $scope.lstImages = JSON.parse($scope.product.MoreImage);
            }, function (error) {
                notificationService.displayError("Không lấy được dữ liệu !");
            });
        }
        DetailProduct();
        
        // GetSEOTitle
        $scope.getSeoTitle = getSeoTitle;
        function getSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                });
                $scope.product.Image = fileUrl;
            }
            finder.popup();
        }

        $scope.lstImages = [];
        $scope.ChooseMoreImages = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.lstImages.push(fileUrl);
                });
            }
            finder.popup();
        }
    }
})(angular.module('bigshop.products'));