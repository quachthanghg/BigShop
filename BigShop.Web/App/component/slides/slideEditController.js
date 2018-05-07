(function (app) {
    app.controller('slideEditController', slideEditController)
    slideEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams','commonService']
    function slideEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
        // Load combobox
        $scope.slide = {
        }
        

        // Update data
        $scope.UpdateSlide = UpdateSlide;
        function UpdateSlide() {
            apiService.put('/Api/Slide/Update', $scope.slide, function (result) {
                notificationService.displaySuccess(result.data.Name + " đã được cập nhật");
                $state.go('slides')
            }, function (error) {
                notificationService.displayError("Cập nhật không được thành công !");
            });
        }

        // Load detail
        $scope.DetailSlide = DetailSlide;
        function DetailSlide() {
            apiService.get('/Api/Slide/GetById/' + $stateParams.id, null, function (result) {
                $scope.slide = result.data;
                $scope.lstImages = JSON.parse($scope.slide.MoreImage);
            }, function (error) {
                notificationService.displayError("Không lấy được dữ liệu !");
            });
        }
        DetailSlide();
        
        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.slide.Image = fileUrl;
                });
                $scope.slide.Image = fileUrl;
            }
            finder.popup();
        }
    }
})(angular.module('bigshop.slides'));