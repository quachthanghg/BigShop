(function (app) {
    app.controller('productListController', productListController);

    productListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function productListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        // GetAll
        $scope.lstProduct = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getListProduct = getListProduct;
        function getListProduct(page) {
            page = page || 0;
            var config = {
                params: {
                    page: page,
                    pageSize: 5
                }
            };
            apiService.get('/Api/Product/GetAll', config, function (result) {
                $scope.lstProduct = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pageCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.TotalPages = Math.ceil($scope.totalCount / $scope.pageCount);
            }, function () {
                console.log("Error !");
            });
        }
        $scope.getListProduct();
        // End GetAll
        // Search
        $scope.filter = '';
        function Search(filter, page) {
            page = page || 0;
            var config = {
                params: {
                    filter: $scope.filter,
                    page: page,
                    pageSize: 2
                }
            };
            apiService.get('/Api/Product/Search', config, function (result) {
                if (result.data.TotalCount > 0) {
                    notificationService.displaySuccess("Đã tìm thấy " + result.data.TotalCount + " bản ghi");
                }
                else {
                    notificationService.displayWarning("Đã tìm thấy " + result.data.TotalCount + " bản ghi");
                }
                $scope.lstProduct = result.data.Items;
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
                apiService.del('/Api/Product/Delete', config, function () {
                    notificationService.displaySuccess = 'Xóa thành công';
                    getListProduct();
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
                angular.forEach($scope.lstProduct, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.lstProduct, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("lstProduct", function (n, o) {
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
            apiService.del('/Api/Product/DeleteMulti', config, function (result) {
                notificationService.displaySuccess('Đã xóa ' + result.data + ' bản ghi')
                getListProduct();
            }, function (error) {
                notificationService.displayError('xóa không thành công');
            });
        }

    }
})(angular.module('bigshop.products'));