(function (app) {
    app.controller('orderDetailListController', orderDetailListController);

    orderDetailListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function orderDetailListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        // GetAll
        $scope.lstOrderDetail = [];
        $scope.page = 0;
        $scope.pageCount = 0;

        $scope.orderDetailID = 0;
        
        $scope.getListOrderDetail = getListOrderDetail;
        function getListOrderDetail(page) {
            page = page || 0;
            var config = {
                params: {
                    page: page,
                    pageSize: 10
                }
            };
            $scope.loading = true;
            apiService.get('/Api/OrderDetail/GetAll', config, function (result) {
                $scope.lstOrderDetail = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pageCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.TotalPages = Math.ceil($scope.totalCount / $scope.pageCount);
                $scope.loading = false;
            }, function () {
                console.log("Error !");
            });
        }
        $scope.getListOrderDetail();
        // End GetAll
        // Search
        $scope.filter = '';
        function Search(filter, page) {
            page = page || 0;
            var config = {
                params: {
                    filter: $scope.filter,
                    page: page,
                    pageSize: 10
                }
            };
            apiService.get('/Api/OrderDetail/Search', config, function (result) {
                if (result.data.TotalCount > 0) {
                    notificationService.displaySuccess("Đã tìm thấy " + result.data.TotalCount + " bản ghi");
                }
                else {
                    notificationService.displayWarning("Đã tìm thấy " + result.data.TotalCount + " bản ghi");
                }
                $scope.lstOrderDetail = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pageCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.TotalPages = Math.ceil($scope.totalCount / $scope.pageCount);
            }, function () {
                console.log("Error !");
            });
        }
        $scope.Search = Search;

        // Delete item
        $scope.del = del;
        function del(id) {
            $ngBootbox.confirm('Bạn có chắc chắn muốn xóa không').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                };
                apiService.del('/Api/OrderDetail/Delete', config, function () {
                    notificationService.displaySuccess = 'Xóa thành công';
                    getListOrderDetail();
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
                angular.forEach($scope.lstOrderDetail, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.lstOrderDetail, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("lstOrderDetail", function (n, o) {
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
            apiService.del('/Api/OrderDetail/DeleteMulti', config, function (result) {
                notificationService.displaySuccess('Đã xóa ' + result.data + ' bản ghi')
                getListOrderDetail();
            }, function (error) {
                notificationService.displayError('xóa không thành công');
            });
        }

    }
})(angular.module('bigshop.orderdetails'));