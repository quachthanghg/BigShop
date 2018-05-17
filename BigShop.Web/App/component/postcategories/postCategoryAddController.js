(function (app) {
    app.controller('postCategoryAddController', postCategoryAddController)
    postCategoryAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService']
    function postCategoryAddController($scope, apiService, notificationService, $state, commonService) {
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

        // Create data
        $scope.AddPostCategory = AddPostCategory;
        function AddPostCategory() {
            $scope.postCategory.MoreImage = JSON.stringify($scope.lstImages);
            apiService.post('/Api/PostCategory/Create', $scope.postCategory, function (result) {
                notificationService.displaySuccess(result.data.Name + " đã được thêm thành công");
                $state.go('postCategories')
            }, function (error) {
                notificationService.displayError("Thêm mới không được thêm mới");
            });
        }

        // GetSEOTitle
        $scope.getSeoTitle = getSeoTitle;
        function getSeoTitle() {
            $scope.postCategory.Alias = commonService.getSeoTitle($scope.postCategory.Name);
        }

        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
        }

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.postCategory.Image = fileUrl;
                });
            }
            finder.popup();
        }
    }
})(angular.module('bigshop.postCategories'));