(function (app) {
    app.controller('applicationUserAddController', applicationUserAddController)
    applicationUserAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService']
    function applicationUserAddController($scope, apiService, notificationService, $state, commonService) {
        // Load combobox
        $scope.applicationUser = {
            applicationGroups: []
        }
        $scope.applicationGroups = [];

        // Create data
        $scope.AddApplicationUser = AddApplicationUser;
        function AddApplicationUser() {
            apiService.post('/Api/ApplicationUser/Create', $scope.applicationUser, function (result) {
                console.log($scope.applicationUser);
                notificationService.displaySuccess(result.data.FullName + " đã được thêm thành công");
                $state.go('applicationUsers')
            }, function (error) {
                notificationService.displayError("Thêm mới không được thêm mới");
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

    }
})(angular.module('bigshop.applicationUsers'));