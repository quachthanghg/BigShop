(function (app) {
    app.controller('homeController', homeController);
    homeController.$inject = ['$scope', 'apiService', '$timeout', '$filter'];
    function homeController($scope, apiService, $timeout, $filter) {
        $scope.fromDate = [];
        $scope.toDate = [];
        function getStatistic() {
            var config = {
                params: {
                    fromDate: '01/05/2018',
                    toDate: '12/12/2018'
                }
            };
            apiService.get('/Api/Statistic/GetRevenueStatistic', config, function (result) {
                var labels = [];
                var revenues = [];
                var benefits = [];

                $.each(result.data, function (i, item) {
                    labels.push(item.Date);
                    revenues.push(item.Revenue);
                    benefits.push(item.Benefit);
                });

                Highcharts.chart('container', {
                    chart: {
                        type: 'column'
                    },
                    title: {
                        text: ''
                    },
                    subtitle: {
                        text: ''
                    },
                    xAxis: {
                        categories: labels,
                        crosshair: true
                    },
                    yAxis: {
                        min: 0,
                        title: {
                            text: 'Doanh thu (VNĐ)'
                        }
                    },
                    tooltip: {
                        headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                        pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:.1f} mm</b></td></tr>',
                        footerFormat: '</table>',
                        shared: true,
                        useHTML: true
                    },
                    plotOptions: {
                        column: {
                            pointPadding: 0.2,
                            borderWidth: 0
                        }
                    },
                    series: [{
                        name: 'Doanh thu',
                        data: revenues

                    }, {
                        name: 'Lợi nhuận',
                        data: benefits

                    }]
                });
                //$scope.labels = labels;
            }, function () {
                console.log("Error !");
            });
        }
        getStatistic();
        
        function GetTop() {
            apiService.get('/Api/Statistic/TopSaleProduct', null, function (result) {
                
                var nameProduct = [];
                var totalQuantity = [];
                var Quantity = [];

                $.each(result.data, function (i, item) {
                    nameProduct.push(item.Name);
                    totalQuantity.push(item.Quantity); 
                    Quantity.push(item.Quantity); 
                });

                Highcharts.chart('container2', {
                    chart: {
                        type: 'bar'
                    },
                    title: {
                        text: ''
                    },
                    xAxis: {
                        categories: nameProduct
                    },
                    yAxis: {
                        min: 0,
                        title: {
                            text: 'Số lượng'
                        }
                    },
                    legend: {
                        reversed: true
                    },
                    plotOptions: {
                        series: {
                            stacking: 'normal'
                        }
                    },
                    series: [{
                        name: 'Số lượng bán ra',
                        data: Quantity
                    }]
                });
                
            }, function () {
                console.log("Error !");
            });
        }
        GetTop();

        $scope.lstProductNotBuy = [];
        function GetProductNotBuy() {
            apiService.get('/Api/Statistic/ProductNotBuy', null, function (result) {
                $scope.lstProductNotBuy = result.data;
                
            }, function () {
                console.log("Error !");
            });
        }
        GetProductNotBuy();
        
        function ProductIsPhone() {
            apiService.get('/Api/Statistic/ProductIsPhone', null, function (result) {

                var nameProduct = [];
                var total = [];

                $.each(result.data, function (i, item) {
                    nameProduct.push(item.Name);
                    total.push(item.TotalProduct);
                });

                Highcharts.chart('container4', {
                    chart: {
                        plotBackgroundColor: null,
                        plotBorderWidth: null,
                        plotShadow: false,
                        type: 'pie'
                    },
                    title: {
                        text: ''
                    },
                    tooltip: {
                        pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                    },
                    plotOptions: {
                        pie: {
                            allowPointSelect: true,
                            cursor: 'pointer',
                            dataLabels: {
                                enabled: false
                            },
                            showInLegend: true
                        }
                    },
                    series: [{
                        name: 'Brands',
                        colorByPoint: true,
                        data: [{
                            name: nameProduct,
                            y: 1
                        }, {
                            name: nameProduct,
                            y: 2
                            }, {
                                name: nameProduct,
                                y: 2
                        }, {
                            name: nameProduct,
                            y: 2
                        }]
                    }]
                });
            }, function () {
                console.log("Error !");
            });
        }
        ProductIsPhone();

        function ProductIsTablet() {
            apiService.get('/Api/Statistic/ProductIsTablet', null, function (result) {
                $scope.lstProductIsTablet = result.data;
                Highcharts.chart('container5', {
                    chart: {
                        plotBackgroundColor: null,
                        plotBorderWidth: null,
                        plotShadow: false,
                        type: 'pie'
                    },
                    title: {
                        text: ''
                    },
                    tooltip: {
                        pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                    },
                    plotOptions: {
                        pie: {
                            allowPointSelect: true,
                            cursor: 'pointer',
                            dataLabels: {
                                enabled: false
                            },
                            showInLegend: true
                        }
                    },
                    series: [{
                        name: 'Brands',
                        colorByPoint: true,
                        data: [{
                            name: 'Chrome',
                            y: 61.41,
                            sliced: true,
                            selected: true
                        }, {
                            name: 'Internet Explorer',
                            y: 11.84
                        }, {
                            name: 'Firefox',
                            y: 10.85
                        }, {
                            name: 'Edge',
                            y: 4.67
                        }, {
                            name: 'Safari',
                            y: 4.18
                        }, {
                            name: 'Other',
                            y: 7.05
                        }]
                    }]
                });
            }, function () {
                console.log("Error !");
            });
        }
        ProductIsTablet();

        function ProductIsLaptop() {
            apiService.get('/Api/Statistic/ProductIsLaptop', null, function (result) {
                $scope.lstProductIsLaptopy = result.data;
                Highcharts.chart('container6', {
                    chart: {
                        plotBackgroundColor: null,
                        plotBorderWidth: null,
                        plotShadow: false,
                        type: 'pie'
                    },
                    title: {
                        text: ''
                    },
                    tooltip: {
                        pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                    },
                    plotOptions: {
                        pie: {
                            allowPointSelect: true,
                            cursor: 'pointer',
                            dataLabels: {
                                enabled: false
                            },
                            showInLegend: true
                        }
                    },
                    series: [{
                        name: 'Brands',
                        colorByPoint: true,
                        data: [{
                            name: 'Chrome',
                            y: 61.41,
                            sliced: true,
                            selected: true
                        }, {
                            name: 'Internet Explorer',
                            y: 11.84
                        }, {
                            name: 'Firefox',
                            y: 10.85
                        }, {
                            name: 'Edge',
                            y: 4.67
                        }, {
                            name: 'Safari',
                            y: 4.18
                        }, {
                            name: 'Other',
                            y: 7.05
                        }]
                    }]
                });
            }, function () {
                console.log("Error !");
            });
        }
        ProductIsLaptop();
        
    }
})(angular.module('bigshop.common'));