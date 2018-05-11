(function (app) {
    app.controller('postAddController', postAddController)
    postAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService']
    function postAddController($scope, apiService, notificationService, $state, commonService) {
        // Load combobox
        $scope.post = {}
        $scope.parentCategories = [];
        function loadParentCategories() {
            apiService.get('/Api/Post/GetAllParents', null, function (result) {
                $scope.parentCategories = result.data;
            }, function () {
                console.log("Load fail !");
            });
        }
        loadParentCategories();

        // Create data
        $scope.AddPost = AddPost;
        function AddPost() {
            apiService.post('/Api/Post/Create', $scope.post, function (result) {
                notificationService.displaySuccess(result.data.Name + " đã được thêm thành công");
                $state.go('Posts')
            }, function (error) {
                notificationService.displayError("Thêm mới không được thêm mới");
            });
        }

        // GetSEOTitle
        $scope.getSeoTitle = getSeoTitle;
        function getSeoTitle() {
            $scope.post.Alias = commonService.getSeoTitle($scope.post.Name);
        }

        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
        }

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.post.Image = fileUrl;
                });
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