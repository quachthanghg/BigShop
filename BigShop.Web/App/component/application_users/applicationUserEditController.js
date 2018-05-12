(function (app) {
    app.controller('applicationUserEditController', applicationUserEditController)
    applicationUserEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams','commonService']
    function applicationUserEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
        // Load combobox
        $scope.applicationUser = {}
        $scope.parentCategories = [];
        function loadParentCategories() {
            apiService.get('/Api/ApplicationUser/GetAllParents', null, function (result) {
                $scope.parentCategories = result.data;
            }, function () {
                console.log("Load fail !");
            });
        }
        loadParentCategories();

        // Update data
        $scope.UpdateApplicationUser = UpdateApplicationUser;
        function UpdateApplicationUser() {
            apiService.put('/Api/ApplicationUser/Update', $scope.applicationUser, function (result) {
                notificationService.displaySuccess(result.data.Name + " đã được cập nhật");
                $state.go('products')
            }, function (error) {
                notificationService.displayError("Cập nhật không được thành công !");
            });
        }

        // Load detail
        $scope.GetByID = GetByID;
        function GetByID() {
            apiService.get('/Api/ApplicationUser/GetById/' + $stateParams.id, null, function (result) {
                $scope.applicationUser = result.data;
            }, function (error) {
                notificationService.displayError("Không lấy được dữ liệu !");
            });
        }
        GetByID();
        
    }
})(angular.module('bigshop.applicationUsers'));