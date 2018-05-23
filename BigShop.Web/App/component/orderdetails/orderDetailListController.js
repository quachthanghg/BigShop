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
        $scope.alertTaoDay = function (item) {
            console.log(item);
            //var content = "<table>" + $("#table").html() + item.Order.CustomerName + "<table>";
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html>');
            printWindow.document.write('<head>');
            printWindow.document.write('<title>Hóa đơn thanh toán</title>');
            printWindow.document.write('</head>');
            printWindow.document.write('<body >');
            printWindow.document.write('<p>');
            printWindow.document.write('Họ và tên: ');
            printWindow.document.write(item.Order.CustomerName);
            printWindow.document.write('</p>');
            printWindow.document.write('<p>');
            printWindow.document.write('Email: ');
            printWindow.document.write(item.Order.CustomerEmail);
            printWindow.document.write('</p>');
            printWindow.document.write('<p>');
            printWindow.document.write('Địa chỉ: ');
            printWindow.document.write(item.Order.CustomerAddress);
            printWindow.document.write('</p>');
            printWindow.document.write('<p>');
            printWindow.document.write('Số điện thoại: ');
            printWindow.document.write(item.Order.CustomerMobile);
            printWindow.document.write('</p>');
            printWindow.document.write('<table style="border: 1px solid black">');
            printWindow.document.write('<tr style="border: 1px solid black">');
            printWindow.document.write('<th>');
            printWindow.document.write('<td>Sản phẩm</td>');
            printWindow.document.write('<td>Giá</td>');
            printWindow.document.write('<td>Số lượng</td>');
            printWindow.document.write('<td>Tổng tiền</td>');
            printWindow.document.write('</th>');
            printWindow.document.write('</tr>');
            printWindow.document.write('<tr style="border: 1px solid black">');
            printWindow.document.write('<th>');
            printWindow.document.write('<td>' + item.Product.Name +'</td>');
            printWindow.document.write('<td>' + item.Product.Price +'</td>');
            printWindow.document.write('<td>' + item.Order.Quantity + '</td>');
            printWindow.document.write('<td>' + item.Product.Price * item.Order.Quantity+ '</td>');
            printWindow.document.write('</th>');
            printWindow.document.write('</tr>');
            printWindow.document.write('</table>');
           // printWindow.document.write(content);
            printWindow.document.write('</body>');
            printWindow.document.write('</html>');
            printWindow.document.close();
            printWindow.print();
        }

    }
})(angular.module('bigshop.orderdetails'));