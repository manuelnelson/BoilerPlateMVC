//Loading contoller
var LoadingCtrl = function ($scope, loadingService) {
    $scope.message = Application.properties.defaultMessage;
    $scope.$watch(function () { return loadingService.isLoading(); }, function (value) {
        $scope.message = loadingService.message;
        $scope.loading = value;
    });
};
LoadingCtrl.$inject = ['$scope', 'loadingService'];