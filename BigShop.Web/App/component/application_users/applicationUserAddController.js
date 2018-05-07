(function (app) {
    app.controller('applicationUserAddController', applicationUserAddController)
    applicationUserAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService']
    function applicationUserAddController($scope, apiService, notificationService, $state, commonService) {
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

        //function loadGroups() {
        //    apiService.get('/Api/ApplicationGroup/GetListAll',
        //        null,
        //        function (response) {
        //            $scope.applicationGroups = response.data;
        //        }, function (response) {
        //            notificationService.displayError('Không tải được danh sách nhóm.');
        //        });

        //}

        //loadGroups();

    }
})(angular.module('bigshop.applicationUsers'));