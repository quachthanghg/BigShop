(function (app) {
    'use strict';
    app.factory('authData', [function () {
        var authDataFactory = {};

        var authentication = {
            // chua dang nhap
            IsAuthenticated: false,
            userName: ""
        };
        authDataFactory.authenticationData = authentication;

        return authDataFactory;
    }]);
})(angular.module('bigshop.common'));