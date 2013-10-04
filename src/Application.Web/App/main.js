window.Application = window.Application || {};
//put specific application properties here
Application.properties = {
    messageType: {
        Warning: '',
        Success: 'alert-success',
        Error: 'alert-danger'
    },
    defaultMessage: 'Loading...' 
};

//Loading App
var TodoApp = angular.module("TodoApp", ["ngResource", "ngRoute", 'ui.bootstrap'], ["$routeProvider", function ($routeProvider) {
    $routeProvider.
        when('/', { controller: TodoListCtrl, templateUrl: '/app/Todo/List/List.html' }).
        when('/edit/:todoId', { controller: TodoEditCtrl, templateUrl: '/app/Todo/Edit/Edit.html' }).
        otherwise({ redirectTo: '/' });
}]); 
