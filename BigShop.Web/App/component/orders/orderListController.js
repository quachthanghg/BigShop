(function (app) {
    app.controller('orderListController', orderListController);

    orderListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function orderListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        // GetAll
        $scope.lstOrder = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        
        $scope.getListOrder = getListOrder;
        function getListOrder(page) {
            page = page || 0;
            var config = {
                params: {
                    page: page,
                    pageSize: 10
                }
            };
            $scope.loading = true;
            apiService.get('/Api/Order/GetAll', config, function (result) {
                $scope.lstOrder = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pageCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.TotalPages = Math.ceil($scope.totalCount / $scope.pageCount);
                $scope.loading = false;
            }, function () {
                console.log("Error !");
            });
        }
        $scope.getListOrder();
        // End GetAll

        // Delete item
        $scope.del = del;
        function del(id) {
            var config = {
                params: {
                    id: id
                }
            };
            apiService.del('/Api/Order/RemoveOrder', config, function () {
                notificationService.displaySuccess = 'Xóa thành công';
                getListOrder();
            }, function () {
                notificationService.displayError = 'Xóa không thành công'
            });
        }
        

    }
})(angular.module('bigshop.orders'));