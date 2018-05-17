(function (app) {
    app.controller('applicationGroupEditController', applicationGroupEditController)
    applicationGroupEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams', 'commonService']
    function applicationGroupEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
        // Load combobox
        $scope.applicationGroup = {
            applicationRoles: []
        }
        $scope.roles = [];

        // Update data
        $scope.UpdateApplicationGroup = UpdateApplicationGroup;
        function UpdateApplicationGroup() {
            apiService.put('/Api/ApplicationGroup/Update', $scope.applicationGroup, function (result) {
                notificationService.displaySuccess(result.data.Name + " đã được cập nhật");
                $state.go('applicationGroups')
            }, function (error) {
                notificationService.displayError("Cập nhật không được thành công !");
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

        // Load detail
        $scope.GetById = GetById;
        function GetById() {
            apiService.get('/Api/ApplicationGroup/GetById/' + $stateParams.id, null, function (result) {
                $scope.applicationGroup = result.data;
            }, function (error) {
                notificationService.displayError("Không lấy được dữ liệu !");
            });
        }
        GetById();
    }
})(angular.module('bigshop.applicationGroups'));