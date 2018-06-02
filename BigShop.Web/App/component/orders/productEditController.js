(function (app) {
    app.controller('orderInfoController', orderInfoController)
    orderInfoController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams','commonService']
    function orderInfoController($scope, apiService, notificationService, $state, $stateParams, commonService) {
        // Load combobox
        $scope.order = {};

        // Load detail
        $scope.DetailOrder = DetailOrder;
        function DetailOrder() {
            apiService.get('/Api/Order/GetById/' + $stateParams.id, null, function (result) {
                $scope.order = result.data;
            }, function (error) {
                notificationService.displayError("Không lấy được dữ liệu !");
            });
        }
        DetailOrder();
    }
})(angular.module('bigshop.products'));