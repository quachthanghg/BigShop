(function (app) {
    app.service('apiService', apiService);

    apiService.$inject = ['$http', 'notificationService', 'authenticationService'];

    function apiService($http, notificationService, authenticationService) {
        return {
            get: get,
            post: post,
            put: put,
            del: del
        }
        // Get
        function get(url, data, success, failure) {
            authenticationService.setHeader();
            $http.get(url, data).then(function (result) {
                success(result);
            }, function (error) {
                failure(error);
            });
        }

        // Post
        function post(url, params, success, failure) {
            authenticationService.setHeader();
            $http.post(url, params).then(function (result) {
                success(result);
            }, function (error) {
                if (error.status === 401) {
                    notificationService.displayError("Authenticate is required !");
                }
                else if (failure != null) {
                    failure(error);
                }
                
            });
        }

        // Put
        function put(url, params, success, failure) {
            authenticationService.setHeader();
            $http.put(url, params).then(function (result) {
                success(result);
            }, function (error) {
                if (error.status === 401) {
                    notificationService.displayError("Authenticate is required !");
                }
                else if (failure != null) {
                    failure(error);
                }

            });
        }

        // Delete
        function del(url, data, success, failure) {
            authenticationService.setHeader();
            $http.delete(url, data).then(function (result) {
                success(result);
            }, function (error) {
                if (error.staus === 401) {
                    notificationService.displayError('Authenticate is required');
                }
                else (failure != null)
                {
                    failure(error);
                };

            });
        }
    };
})(angular.module('bigshop.common'));