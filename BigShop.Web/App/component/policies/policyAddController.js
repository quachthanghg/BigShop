(function (app) {
    app.controller('policyAddController', policyAddController)
    policyAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService']
    function policyAddController($scope, apiService, notificationService, $state, commonService) {
        // Load combobox
        $scope.policy = {}

        // Create data
        $scope.AddPolicy = AddPolicy;
        function AddPolicy() {
            apiService.post('/Api/Policy/Create', $scope.policy, function (result) {
                notificationService.displaySuccess(result.data.Name + " đã được thêm thành công");
                $state.go('policies')
            }, function (error) {
                notificationService.displayError("Thêm mới không thành công");
            });
        }

        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
        }
    }
})(angular.module('bigshop.policies'));