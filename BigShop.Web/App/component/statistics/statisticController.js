(function (app) {
    app.controller('statisticController', statisticController)
    statisticController.$inject = ['$scope', 'apiService', '$timeout', '$filter']
    function statisticController($scope, apiService, $timeout, $filter) {

        // Simulate async data update
        $scope.data = [];
        $scope.labels = [];
        $scope.series = ['Series A', 'Series B'];
        function getStatistic() {
            var config = {
                params: {
                    fromDate: '01/05/2018',
                    toDate: new Date()
                }
            };
            apiService.get('/Api/Statistic/GetRevenueStatistic', config, function (result) {
                //var labels = [];
                //var data = [];
                //var revenues = [];
                //var benefits = [];
                //$.each(result.data, function (i, item) {
                //    labels.push($filter('date')(item.Date, 'dd/MM/yyyy'));
                //    revenues.push(item.Revenue);
                //    benefits.push(item.Benefit);
                //});
                //data.push(revenues);
                //data.push(benefits);

                $scope.data = result.data;
                //$scope.labels = labels;
            }, function () {
                console.log("Error !");
            });
        }

        getStatistic();
    }
})(angular.module('bigshop.statistics'));