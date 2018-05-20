(function (app) {
    app.controller('saleAddController', saleAddController)
    saleAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService']
    function saleAddController($scope, apiService, notificationService, $state, commonService) {
        // Load combobox
        $scope.sale= {}
        $scope.parentCategories = [];
        function loadParentCategories() {
            apiService.get('/Api/Sale/GetAllParents', null, function (result) {
                $scope.parentCategories = result.data;
            }, function () {
                console.log("Load fail !");
            });
        }
        loadParentCategories();

        // Create data
        $scope.AddSale = AddSale;
        function AddSale() {
            apiService.post('/Api/Sale/Create', $scope.sale, function (result) {
                notificationService.displaySuccess(result.data.Name + " đã được thêm thành công");
                $state.go('sales')
            }, function (error) {
                notificationService.displayError("Thêm mới không được thêm mới");
            });
        }

        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
        }
        
    }
})(angular.module('bigshop.sales'));