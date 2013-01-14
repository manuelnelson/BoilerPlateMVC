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
//Loading App

var TodoApp = angular.module("TodoApp", ["ngResource"], ["$routeProvider",  function ($routeProvider) {
    $routeProvider.
        when('/', { controller: TodoListCtrl, templateUrl: '/Templates/TodoList.html' }).
        when('/edit/:todoId', { controller: TodoEditCtrl, templateUrl: '/Templates/TodoEdit.html' }).
        otherwise({ redirectTo: '/' });
}]); 
