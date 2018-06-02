(function (app) {
    app.controller('orderInfoController', orderInfoController);
    orderInfoController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams', 'commonService'];
    function orderInfoController($scope, apiService, notificationService, $state, $stateParams, commonService) {

        $scope.order = [];
        $scope.lstOrderDetails = [];

        $scope.GetOrderById = GetOrderById;
        function GetOrderById() {
            apiService.get('/Api/Order/GetOrderById/' + $stateParams.id, null, function (result) {
                $scope.order = result.data;
            }, function (error) {
                notificationService.displayError("Không lấy được dữ liệu !");
            });
        }
        GetOrderById()

        // Load detail
        $scope.GetOrderDetailById = function () {
            apiService.get('/Api/OrderDetail/GetOrderDetailById/' + $stateParams.id, null, function (result) {
                $scope.lstOrderDetails = result.data;
            }, function (error) {
                notificationService.displayError("Không lấy được dữ liệu !");
            });
        }
        $scope.GetOrderDetailById();

        $scope.print_order = function (order) {
            var innerContents = document.getElementById(order).innerHTML;
            var popupWinindow = window.open();
            popupWinindow.document.open();
            popupWinindow.document.write('<html><head><link rel="stylesheet" type="text/css" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" /><script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script></head><body onload="window.print()">' + innerContents + '</html>');
            popupWinindow.document.close();
        }
    }
})(angular.module('bigshop.orders'));