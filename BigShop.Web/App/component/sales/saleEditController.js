(function (app) {
    app.controller('saleEditController', saleEditController)
    saleEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams','commonService']
    function saleEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
        // Load combobox
        $scope.sale = {}
        $scope.parentCategories = [];
        function loadParentCategories() {
            apiService.get('/Api/Sale/GetAllParents', null, function (result) {
                $scope.parentCategories = result.data;
            }, function () {
                console.log("Load fail !");
            });
        }
        loadParentCategories();

        // Update data
        $scope.UpdateSale = UpdateSale;
        function UpdateSale() {
            apiService.put('/Api/Sale/Update', $scope.sale, function (result) {
                notificationService.displaySuccess(result.data.Name + " đã được cập nhật");
                $state.go('sales')
            }, function (error) {
                notificationService.displayError("Cập nhật không được thành công !");
            });
        }

        // Load detail
        $scope.DetailSale = DetailSale;
        function DetailSale() {
            apiService.get('/Api/Sale/GetById/' + $stateParams.id, null, function (result) {
                $scope.sale = result.data;
            }, function (error) {
                notificationService.displayError("Không lấy được dữ liệu !");
            });
        }
        DetailSale();
    }
})(angular.module('bigshop.sales'));