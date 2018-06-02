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
                    labels.push($filter('date')(item.Date, 'dd/MM/yyyy'));
                    revenues.push(item.Revenue);
                    benefits.push(item.Benefit);
                });

                Highcharts.chart('container', {
                    chart: {
                        type: 'column'
                    },
                    title: {
                        text: 'Doanh thu theo ngày'
                    },
                    subtitle: {
                        text: 'Website: BigShop.com'
                    },
                    xAxis: {
                        categories: labels,
                        crosshair: true
                    },
                    yAxis: {
                        min: 0,
                        title: {
                            text: 'Doanh thu ($$$)'
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

        $scope.lstHot = [];

        function GetTop() {
            apiService.get('/Api/Statistic/TopSaleProduct', null, function (result) {
                $scope.lstHot = result.data;
                //var name = [];
                //var price = [];
                //var quantityProduct = [];
                //var quantity = [];

                //$.each(result.data, function (i, item) {
                //    name.push(item.Name);
                //    price.push(item.Price);
                //    quantityProduct.push(item.QuantityProduct)
                //    quantity.push(item.Quantity)
                //});

                //Highcharts.chart('container', {
                //    chart: {
                //        type: 'bar'
                //    },
                //    title: {
                //        text: 'Stacked bar chart'
                //    },
                //    xAxis: {
                //        categories: name
                //    },
                //    yAxis: {
                //        min: 0,
                //        title: {
                //            text: 'Total fruit consumption'
                //        }
                //    },
                //    legend: {
                //        reversed: true
                //    },
                //    plotOptions: {
                //        series: {
                //            stacking: 'normal'
                //        }
                //    },
                //    series: [{
                //        name: 'Đã bán',
                //        data: quantity
                //    }, {
                //        name: 'Tổng số',
                //        data: quantityProduct
                //    }
                //});

                //var name = [];
                //var price = [];
                //var quantityProduct = [];
                //var quantity = [];

                //$.each(result.data, function (i, item) {
                //    name.push(item.Name);
                //    price.push(item.Price);
                //    quantityProduct.push(item.QuantityProduct)
                //    quantity.push(item.Quantity)
                //});

                //Highcharts.chart('container1', {
                //    chart: {
                //        type: 'column'
                //    },
                //    title: {
                //        text: 'Browser market shares. January, 2018'
                //    },
                //    subtitle: {
                //        text: 'Click the columns to view versions. Source: <a href="http://statcounter.com" target="_blank">statcounter.com</a>'
                //    },
                //    xAxis: {
                //        type: 'category'
                //    },
                //    yAxis: {
                //        title: {
                //            text: 'Total percent market share'
                //        }

                //    },
                //    legend: {
                //        enabled: false
                //    },
                //    plotOptions: {
                //        series: {
                //            borderWidth: 0,
                //            dataLabels: {
                //                enabled: true,
                //                format: '{point.y:.1f}%'
                //            }
                //        }
                //    },

                //    tooltip: {
                //        headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                //        pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}%</b> of total<br/>'
                //    },

                //    "series": [
                //        {
                //            "name": "Browsers",
                //            "colorByPoint": true,
                //            "data": [
                //                {
                //                    "name": "Chrome",
                //                    "y": quantity,
                //                    "drilldown": "Chrome"
                //                },
                //                {
                //                    "name": "Firefox",
                //                    "y": 10.57,
                //                    "drilldown": "Firefox"
                //                },
                //                {
                //                    "name": "Internet Explorer",
                //                    "y": 7.23,
                //                    "drilldown": "Internet Explorer"
                //                },
                //                {
                //                    "name": "Safari",
                //                    "y": 5.58,
                //                    "drilldown": "Safari"
                //                },
                //                {
                //                    "name": "Edge",
                //                    "y": 4.02,
                //                    "drilldown": "Edge"
                //                }
                //            ]
                //        }
                //    ]
                //});


            }, function () {
                console.log("Error !");
            });
        }
        GetTop();
    }


})(angular.module('bigshop.common'));