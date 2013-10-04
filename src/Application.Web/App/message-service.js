////Message Service
TodoApp.factory('messageService', ['$location', function ($location) {
    var service = {
        messageOptions: {
            title: '',
            message: '',
            alertType: Application.properties.messageType.Warning
        },
        showMessage: function (title, message, type) {
            service.messageOptions.alertType = (typeof type === "undefined") ? Application.properties.messageType.Warning : type;
            service.messageOptions.title = title;
            service.messageOptions.message = message;
            service.show = true;
        },
        isShowing: function () {
            return service.show;
        },
        closeMessage: function () {
            service.show = false;
        },
        handleError: function (response) {
            switch (response.status) {
                case 400://bad request
                    //Should have a response status. Parse into json, display message
                    service.showMessage("Not so fast", response.data.ResponseStatus.Message, Application.properties.messageType.Error);
                    return false;
                case 401://unauthorized 
                    //go to unauthorized page
                    if ($location.path() === '/Account/Login/') {
                        service.showMessage("Invalid Credentials", "Either the e-mail or password you entered is incorrect.");
                        return false;
                    }
                    $location.path('/Account/Unauthorized');
                    //service.showMessage("Unauthorized", "You must be logged in to complete this action. Log in <a href='/Account/LogOn/' title='Log In' >here</a>", Application.properties.messageType.Warning);
                    return false;
                case 403://Forbidden 
                    //go to unauthorized page
                    $location.path('/Account/Unauthorized');
                    return false;
                default:
                    service.showMessage("Uh oh!", "Sorry, we could not process your request.  The error has been logged and we will do our best to correct the error asap.", Application.properties.messageType.Error);
                    return false;
            }
        }
    };
    return service;
}]);