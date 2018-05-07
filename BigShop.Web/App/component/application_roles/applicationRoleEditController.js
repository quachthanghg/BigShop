(function (app) {
    app.controller('applicationRoleEditController', applicationRoleEditController)
    applicationRoleEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams','commonService']
    function applicationRoleEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
        // Load combobox
        $scope.applicationRole = {}

        // Update data
        $scope.UpdateApplicationRole = UpdateApplicationRole;
        function UpdateApplicationRole() {
            apiService.put('/Api/ApplicationRole/Update', $scope.applicationRole, function (result) {
                notificationService.displaySuccess(result.data.Name + " đã được cập nhật");
                $state.go('applicationRoles')
            }, function (error) {
                notificationService.displayError("Cập nhật không được thành công !");
            });
        }

        // Load detail
        $scope.DetailRole = DetailRole;
        function DetailRole() {
            apiService.get('/Api/ApplicationRole/GetById/' + $stateParams.id, null, function (result) {
                $scope.applicationRole = result.data;
            }, function (error) {
                notificationService.displayError("Không lấy được dữ liệu !");
            });
        }
        DetailRole();
    }
})(angular.module('bigshop.applicationRoles'));