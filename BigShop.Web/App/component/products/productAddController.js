(function (app) {
    app.controller('productAddController', productAddController)
    productAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService']
    function productAddController($scope, apiService, notificationService, $state, commonService) {
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

        // Create data
        $scope.AddProduct = AddProduct;
        function AddProduct() {
            $scope.product.MoreImage = JSON.stringify($scope.lstImages);
            apiService.post('/Api/Product/Create', $scope.product, function (result) {
                notificationService.displaySuccess(result.data.Name + " đã được thêm thành công");
                $state.go('products')
            }, function (error) {
                notificationService.displayError("Thêm mới không được thêm mới");
            });
        }

        // GetSEOTitle
        $scope.getSeoTitle = getSeoTitle;
        function getSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }

        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
        }

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                });
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