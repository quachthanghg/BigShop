(function (app) {
    app.controller('applicationUserEditController', applicationUserEditController)
    applicationUserEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams','commonService']
    function applicationUserEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
        // Load combobox
        $scope.applicationUser = {
            applicationGroups: []
        }
        $scope.applicationGroups = [];

        // Update data
        $scope.UpdateApplicationUser = UpdateApplicationUser;
        function UpdateApplicationUser() {
            apiService.put('/Api/ApplicationUser/Update', $scope.applicationUser, function (result) {
                notificationService.displaySuccess(result.data.FullName + " đã được cập nhật");
                $state.go('applicationUsers')
            }, function (error) {
                notificationService.displayError("Cập nhật không được thành công !");
            });
        }

        // Load detail
        $scope.GetByID = GetByID;
        function GetByID() {
            apiService.get('/Api/ApplicationUser/GetById/' + $stateParams.id, null, function (result) {
                $scope.applicationUser = result.data;
                console.log(result.data);
            }, function (error) {
                notificationService.displayError("Không lấy được dữ liệu !");
            });
        }

        function loadGroups() {
            apiService.get('/Api/ApplicationGroup/GetListAll',
                null,
                function (res) {
                    $scope.applicationGroups = res.data;
                    console.log(res.data);
                }, function (res) {
                    notificationService.displayError('Không tải được danh sách nhóm.');
                });

        }
        loadGroups();
        GetByID();
        
    }
})(angular.module('bigshop.applicationUsers'));