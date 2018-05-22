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
            $ngBootbox.confirm('Bạn có chắc chắn muốn HỦY không?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                };
                apiService.del('/Api/Order/Delete', config, function () {
                    notificationService.displaySuccess = 'Xóa thành công';
                    getListOrder();
                }, function () {
                    notificationService.displayError = 'Xóa không thành công'
                });
            });
        }

        // Delete multi
        $scope.delMulti = delMulti;
        $scope.selectAll = selectAll;
        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.lstOrder, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.lstOrder, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("lstOrder", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function delMulti() {
            var listID = [];
            $.each($scope.selected, function (i, item) {
                listID.push(item.ID);
            })
            var config = {
                params: {
                    isCheckID: JSON.stringify(listID)
                }
            };
            apiService.del('/Api/Order/DeleteMulti', config, function (result) {
                notificationService.displaySuccess('Đã xóa ' + result.data + ' bản ghi')
                getListOrder();
            }, function (error) {
                notificationService.displayError('xóa không thành công');
            });
        }

    }
})(angular.module('bigshop.orders'));