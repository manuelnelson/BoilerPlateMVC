//Todoservice
TodoApp.factory('todoService', function ($resource) {
    return $resource('/api/todos/:Id', { Id: '@Id' },
    {
        update: { method: 'PUT' },
        deleteAll: { method: 'DELETE' }
    });
});

//Message Service
MessageApp.factory('messageService', function () {
    var service = {
        messageOptions: {
            title: '', 
            message: '',
            alertType: Application.properties.messageType.Warning
        },
        showMessage: function (title, message, type){
            service.messageOptions.alertType = (typeof type === "undefined") ? Application.properties.messageType.Warning : type;
            service.messageOptions.title = title;
            service.messageOptions.message = message;
            service.show = true;
        },
        isShowing: function(){
            return service.show;
        },
        closeMessage: function() {
            service.show = false;
        },
        handleError: function(response) {
            switch (response.status) { 
                case 400://bad request
                    //Should have a response status. Parse into json, display message
                    service.showMessage("Not so fast", response.data.ResponseStatus.Message, Application.properties.messageType.Error);
                    return false;
                case 401://unauthorized 
                    service.showMessage("Unauthorized", "You must be logged in to complete this action. Log in <a href='/Account/LogOn/' title='Log In' >here</a>", Application.properties.messageType.Warning);
                    return false;
                default:
                    service.showMessage("Uh oh!", "Sorry, we could not process your request.  The error has been logged and we will do our best to correct the error asap.", Application.properties.messageType.Error);
                    return false;
            }
        }
    };
    return service;
});

//Loading Service
//http://plnkr.co/edit/88wTp8?p=preview
LoadingApp.factory('loadingService', function () {
    var service = {
        requestCount: 0,
        isLoading: function () {
            return service.requestCount > 0;
        },
        //use these to manually show loading
        increment: function() {
            service.requestCount++;
        },
        decrement: function() {
            service.requestCount--;
            service.message = Application.properties.defaultMessage;
        },
        message: Application.properties.defaultMessage
    };
    return service;
});

LoadingApp.factory('onStartInterceptor', function (loadingService) {
    return function (data, headersGetter) {
        loadingService.requestCount++;
        return data; 
    };
});

LoadingApp.factory('onCompleteInterceptor', function (loadingService, messageService) {
    return function (promise) {
        //successful response
        var decrementRequestCount = function (response) {
            loadingService.requestCount--;
            //always return to default message
            loadingService.message = Application.properties.defaultMessage;
            return response;
        };
        //Error
        var decrementRequestCountError = function(response) {
            loadingService.requestCount--;
            //return to default message for next loading... call
            loadingService.message = Application.properties.defaultMessage;
            //show error message
            messageService.handleError(response);
            return response;
        };
        return promise.then(decrementRequestCount, decrementRequestCountError);
    };
});

LoadingApp.config(function ($httpProvider) {
    $httpProvider.responseInterceptors.push('onCompleteInterceptor');
});

LoadingApp.run(function ($http, onStartInterceptor) {
    $http.defaults.transformRequest.push(onStartInterceptor);
});