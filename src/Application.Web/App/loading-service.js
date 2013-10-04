TodoApp.factory('loadingService', function () {
    var service = {
        requestCount: 0,
        isLoading: function () {
            return service.requestCount > 0;
        },
        //use these to manually show loading
        increment: function () {
            service.requestCount++;
        },
        decrement: function () {
            service.requestCount--;
            service.message = Application.properties.defaultMessage;
        },
        message: Application.properties.defaultMessage
    };
    return service;
});

//Code modified from http://docs.angularjs.org/api/ng.$http
TodoApp.factory('myHttpInterceptor', ['$q', 'loadingService', 'messageService', function ($q, loadingService, messageService) {
    return {
        // optional method
        'request': function(config) {
            // do something on success
            loadingService.requestCount++;
            return config || $q.when(config);
        },
 
        // optional method
        'response': function(response) {
            // do something on success
            loadingService.requestCount--;
            //always return to default message
            loadingService.message = Application.properties.defaultMessage;
            return response || $q.when(response);
        },
 
        // optional method
        'responseError': function(rejection) {
            // do something on error
            //For now, let's treat errors as errors and not recover   
            loadingService.requestCount--;
            //return to default message for next loading... call
            loadingService.message = Application.properties.defaultMessage;
            //show error message
            messageService.handleError(rejection);
            return $q.reject(rejection);


            //if (canRecover(rejection)) {
            //    return responseOrNewPromise
            //}
        }
    };
}]);
TodoApp.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.interceptors.push('myHttpInterceptor');
}]);
 