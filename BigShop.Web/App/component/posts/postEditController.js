(function (app) {
    app.controller('postEditController', postEditController)
    postEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams','commonService']
    function postEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
        // Load combobox
        $scope.post = {
            CreatedDate: new Date(),
            Status: true
        }
        $scope.parentCategories = [];
        function loadParentCategories() {
            apiService.get('/Api/Post/GetAllParents', null, function (result) {
                $scope.parentCategories = result.data;
            }, function () {
                console.log("Load fail !");
            });
        }
        loadParentCategories();

        // Update data
        $scope.UpdatePost = UpdatePost;
        function UpdatePost() {
            apiService.put('/Api/Post/Update', $scope.post, function (result) {
                notificationService.displaySuccess(result.data.Name + " đã được cập nhật");
                $state.go('posts')
            }, function (error) {
                notificationService.displayError("Cập nhật không được thành công !");
            });
        }

        // Load detail
        $scope.DetailPost = DetailPost;
        function DetailPost() {
            apiService.get('/Api/Post/GetById/' + $stateParams.id, null, function (result) {
                $scope.post = result.data;
                $scope.lstImages = JSON.parse($scope.post.MoreImage);
            }, function (error) {
                notificationService.displayError("Không lấy được dữ liệu !");
            });
        }
        DetailPost();
        
        // GetSEOTitle
        $scope.getSeoTitle = getSeoTitle;
        function getSeoTitle() {
            $scope.post.Alias = commonService.getSeoTitle($scope.post.Name);
        }

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.post.Image = fileUrl;
                });
                $scope.post.Image = fileUrl;
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
})(angular.module('bigshop.Posts'));