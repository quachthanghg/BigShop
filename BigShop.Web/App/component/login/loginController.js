
(function (app) {
    app.controller('loginController', ['$scope', 'loginService', '$injector', 'notificationService',
        function ($scope, loginService, $injector, notificationService) {

            $scope.loginData = {
                userName: "",
                password: ""
            };

            $scope.loginSubmit = function () {
                loginService.login($scope.loginData.userName, $scope.loginData.password)
                    .then(function (response) {
                        if (response !== null && response.data.error == 'invalid_grant') {
                        notificationService.displayError("Tên đăng nhập hoặc mật khẩu không đúng.");
                    }
                    else {
                        var stateService = $injector.get('$state');
                        stateService.go('home');
                    }
                });
            }
        }]);
})(angular.module('bigshop'));