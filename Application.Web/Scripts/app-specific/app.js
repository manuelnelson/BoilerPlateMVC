window.Application = window.Application || {};
//put specific application properties here
Application.properties = {
    messageType: {
        Warning: '',
        Success: 'alert-success',
        Error: 'alert-error'
    },
    defaultMessage: 'Loading...'
};
var TodoApp = angular.module("TodoApp", ["ngResource", "loading", "message"]).
   config(function ($routeProvider) {
       $routeProvider.
           when('/', { controller: TodoListCtrl, templateUrl: '/Templates/TodoList.html' }).
           when('/edit/:todoId', { controller: TodoEditCtrl, templateUrl: '/Templates/TodoEdit.html' }).
           otherwise({ redirectTo: '/' });
   });

//Loading App
var LoadingApp = angular.module('loading', []);
var MessageApp = angular.module('message', []);

//Global Display Error Message
Application.GenericErrorMessage = function (jqXHR, textStatus, errorThrown) {
    //close modals if visible
    //$('.modal').modal('hide');
};
