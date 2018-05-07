(function (app) {
    app.controller('applicationGroupEditController', applicationGroupEditController)
    applicationGroupEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams', 'commonService']
    function applicationGroupEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
        // Load combobox
        $scope.applicationGroup = {}

        // Update data
        $scope.UpdateApplicationGroup = UpdateApplicationGroup;
        function UpdateApplicationGroup() {
            apiService.put('/Api/ApplicationGroup/Update', $scope.applicationGroup, function (result) {
                notificationService.displaySuccess(result.data.Name + " đã được cập nhật");
                $state.go('products')
            }, function (error) {
                notificationService.displayError("Cập nhật không được thành công !");
            });
        }

        // Load detail
        $scope.DetailApplicationGroup = DetailApplicationGroup;
        function DetailApplicationGroup() {
            apiService.get('/Api/ApplicationGroup/GetById/' + $stateParams.id, null, function (result) {
                $scope.product = result.data;
                $scope.lstImages = JSON.parse($scope.product.MoreImage);
            }, function (error) {
                notificationService.displayError("Không lấy được dữ liệu !");
            });
        }
        DetailApplicationGroup();
    }
})(angular.module('bigshop.applicationGroups'));