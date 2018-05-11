(function (app) {
    app.controller('homeController', homeController);
    homeController.$inject = ['$scope', 'apiService', '$timeout', '$filter'];
    function homeController($scope, apiService, $timeout, $filter) {
        function getStatistic() {
            var config = {
                params: {
                    fromDate: '01/05/2018',
                    toDate: new Date()
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
    }
})(angular.module('bigshop.common'));