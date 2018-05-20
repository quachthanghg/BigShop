(function (app) {
    app.controller('policyEditController', policyEditController)
    policyEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams','commonService']
    function policyEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
        // Load combobox
        $scope.policy = {};

        // Update data
        $scope.UpdatePolicy = UpdatePolicy;
        function UpdatePolicy() {
            apiService.put('/Api/Policy/Update', $scope.policy, function (result) {
                notificationService.displaySuccess(result.data.Name + " đã được cập nhật");
                $state.go('policies')
            }, function (error) {
                notificationService.displayError("Cập nhật không được thành công !");
            });
        }

        // Load detail
        $scope.DetailPolicy = DetailPolicy;
        function DetailPolicy() {
            apiService.get('/Api/Policy/GetById/' + $stateParams.id, null, function (result) {
                $scope.policy = result.data;
            }, function (error) {
                notificationService.displayError("Không lấy được dữ liệu !");
            });
        }
        DetailPolicy();
    }
})(angular.module('bigshop.policies'));