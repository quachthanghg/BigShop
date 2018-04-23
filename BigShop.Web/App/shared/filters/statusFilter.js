(function (app) {
    app.filter('statusFilter', function () {
        return function (input) {
            if (input === true) {
                return 'Active';
            } else {
                return 'Block';
            }
        };
    });
})(angular.module('bigshop.common'));