(function (app) {
    app.service('apiService', apiService);

    apiService.$inject = ['$http', 'notificationService'];

    function apiService($http, notificationService) {
        return {
            get: get,
            post: post,
            put: put,
            del: del
        }
        // Get
        function get(url, data, success, failure) {
            $http.get(url, data).then(function (result) {
                success(result);
            }, function (error) {
                failure(error);
            });
        }

        // Post
        function post(url, params, success, failure) {
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
            //authenticationService.setHeader();
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