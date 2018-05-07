(function (app) {
    app.controller('slideAddController', slideAddController)
    slideAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService']
    function slideAddController($scope, apiService, notificationService, $state, commonService) {
        // Load combobox
        $scope.slide = {
        }

        // Create data
        $scope.AddSlide = AddSlide;
        function AddSlide() {
            apiService.post('/Api/Slide/Create', $scope.slide, function (result) {
                notificationService.displaySuccess(result.data.Name + " đã được thêm thành công");
                $state.go('slides')
            }, function (error) {
                notificationService.displayError("Thêm mới không được thành công");
            });
        }

        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
        }

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.slide.Image = fileUrl;
                });
            }
            finder.popup();
        }
    }
})(angular.module('bigshop.slides'));