(function (app) {
    app.controller('applicationGroupAddController', applicationGroupAddController)
    applicationGroupAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService']
    function applicationGroupAddController($scope, apiService, notificationService, $state, commonService) {
        
        $scope.applicationGroup = {
            applicationRoles: []
        }
        $scope.roles = [];

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

        function loadRoles() {
            apiService.get('/Api/ApplicationRole/GetListAll',
                null,
                function (res) {
                    $scope.applicationRoles = res.data;
                    console.log(res.data);
                }, function (res) {
                    notificationService.displayError('Không tải được danh sách nhóm.');
                });
        }
        loadRoles();
        
    }
})(angular.module('bigshop.applicationGroups'));