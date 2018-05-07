(function (app) {
    app.controller('applicationGroupAddController', applicationGroupAddController)
    applicationGroupAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService']
    function applicationGroupAddController($scope, apiService, notificationService, $state, commonService) {
        
        $scope.applicationGroup = {}

        // Create data
        $scope.AddApplicationGroup = AddApplicationGroup;
        function AddApplicationGroup() {
            apiService.post('/Api/ApplicationGroup/Create', $scope.applicationGroup, function (result) {
                notificationService.displaySuccess(result.data.Name + " đã được thêm thành công");
                $state.go('applicationGroups')
            }, function (error) {
                notificationService.displayError("Thêm mới không thành công");
            });
        }
        
    }
})(angular.module('bigshop.applicationGroups'));