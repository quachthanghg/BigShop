(function (app) {
    app.controller('postCategoryEditController', postCategoryEditController)
    postCategoryEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams','commonService']
    function postCategoryEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
        // Load combobox
        $scope.postCategory = {
            CreatedDate: new Date(),
            Status: true
        }
        $scope.parentCategories = [];
        function loadParentCategories() {
            apiService.get('/Api/PostCategory/GetAllParents', null, function (result) {
                $scope.parentCategories = result.data;
            }, function () {
                console.log("Load fail !");
            });
        }
        loadParentCategories();

        // Update data
        $scope.UpdatePostCategory = UpdatePostCategory;
        function UpdatePostCategory() {
            apiService.put('/Api/PostCategory/Update', $scope.postCategory, function (result) {
                notificationService.displaySuccess(result.data.Name + " đã được cập nhật");
                $state.go('postCategories')
            }, function (error) {
                notificationService.displayError("Cập nhật không được thành công !");
            });
        }

        // Load detail
        $scope.DetailPostCategory = DetailPostCategory;
        function DetailPostCategory() {
            apiService.get('/Api/PostCategory/GetById/' + $stateParams.id, null, function (result) {
                $scope.postCategory = result.data;
            }, function (error) {
                notificationService.displayError("Không lấy được dữ liệu !");
            });
        }
        DetailPostCategory();
        
        // GetSEOTitle
        $scope.getSeoTitle = getSeoTitle;
        function getSeoTitle() {
            $scope.postCategory.Alias = commonService.getSeoTitle($scope.postCategory.Name);
        }

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.postCategory.Image = fileUrl;
                });
                $scope.postCategory.Image = fileUrl;
            }
            finder.popup();
        }

        $scope.lstImages = [];
        $scope.ChooseMoreImages = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.lstImages.push(fileUrl);
                });
            }
            finder.popup();
        }
    }
})(angular.module('bigshop.postCategories'));