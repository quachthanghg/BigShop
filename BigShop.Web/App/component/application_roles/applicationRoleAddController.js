(function (app) {
    app.controller('applicationRoleAddController', applicationRoleAddController)
    applicationRoleAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService']
    function applicationRoleAddController($scope, apiService, notificationService, $state, commonService) {
        // Load combobox
        $scope.applicationRole = {
        }
        

        // Create data
        $scope.AddApplicationRole = AddApplicationRole;
        function AddApplicationRole() {
            apiService.post('/Api/ApplicationRole/Create', $scope.applicationRole, function (result) {
                notificationService.displaySuccess(result.data.Name + " đã được thêm thành công");
                $state.go('applicationRoles')
            }, function (error) {
                notificationService.displayError("Thêm mới không được thêm mới");
            });
        }
    }
})(angular.module('bigshop.applicationRoles'));