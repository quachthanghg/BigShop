(function (app) {
    app.controller('orderDetailListController', orderDetailListController);

    orderDetailListController.$inject = ['$scope', 'apiService', 'notificationService', '$stateParams', '$ngBootbox', '$filter'];

    function orderDetailListController($scope, apiService, notificationService, $stateParams, $ngBootbox, $filter) {
        // GetAll
        $scope.lstOrderDetail = [];
        $scope.page = 0;
        $scope.pageCount = 0;


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
                $scope.GetListOrderDetailMerge();
            }, function () {
                console.log("Error !");
            });
        }
        $scope.getListOrderDetail();
        // End GetAll

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
            printWindow.document.write('<td>' + item.Product.Name + '</td>');
            printWindow.document.write('<td>' + item.Product.Price + '</td>');
            printWindow.document.write('<td>' + item.Order.Quantity + '</td>');
            printWindow.document.write('<td>' + item.Product.Price * item.Order.Quantity + '</td>');
            printWindow.document.write('</th>');
            printWindow.document.write('</tr>');
            printWindow.document.write('</table>');
            // printWindow.document.write(content);
            printWindow.document.write('</body>');
            printWindow.document.write('</html>');
            printWindow.document.close();
            printWindow.print();
        }

        $scope.ListOrderDetailMerge = [];
        $scope.GetListOrderDetailMerge = function () {
            for (var i = 0; i < $scope.lstOrderDetail.length; i++) {
                var itemOrder = $scope.lstOrderDetail[i];
                var exists = _.where($scope.ListOrderDetailMerge, { OrderID: itemOrder.OrderID });
                if (exists.length == 0) {
                    var countItemSame = _.where($scope.lstOrderDetail, { OrderID: itemOrder.OrderID}).length;
                    itemOrder.rowspan = countItemSame == 1 ? - 1 : countItemSame;
                    itemOrder.IsDisplay = true;
                    $scope.ListOrderDetailMerge.push(itemOrder);
                } else {
                    var itemSame = _.where($scope.ListOrderDetailMerge, { OrderID: itemOrder.OrderID});
                    if (itemSame.length > 0) {
                        itemOrder.rowspan = -1;
                        itemOrder.rowspan = -1;
                        itemOrder.IsDisplay = false;
                    }
                    $scope.ListOrderDetailMerge.push(itemOrder);
                }
            }
        }

        $scope.lstTmp = [];
        
        $scope.IsDisplay = function (item, index) {
            var lstExist = _.where($scope.lstOrderDetail, { OrderID: item.OrderID});
            if (lstExist.length > 1) {
                if (_.where($scope.lstTmp, { OrderID: item.OrderID, index: index }).length > 0) {
                    return true;
                } else {
                    item.index = index;
                    $scope.lstTmp.push(item);
                    var countTmp = _.where($scope.lstOrderDetail, { OrderID: item.OrderID }).length;
                    item.rowspan = countTmp == 1 ? - 1 : countTmp;
                    console.log(item.rowspan);
                    return true;
                }
            } else {
                item.rowspan = -1;
                return false;
            }
        }


    }
})(angular.module('bigshop.orderdetails'));