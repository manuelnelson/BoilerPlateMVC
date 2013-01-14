var TodoListCtrl = function ($scope, $location, todoService, messageService) {
    $scope.getTodos = function () {
        todoService.query({ format: 'json' }, function (todos) {
            $scope.todos = [];
            $scope.todos = $scope.todos.concat(todos);
        });
    };

    $scope.addTodo = function () {
        todoService.save($scope.todo, function (task) {
            $scope.todos.push(task);
            $scope.todo.Task = '';
        });
    }; 

    $scope.archive = function () {
        var deleteTodos = $.grep($scope.todos, function (todo) {
            return todo.Completed;
        });
        var deleteIds = [];
        angular.forEach(deleteTodos, function (value, key) {
            deleteIds.push(value.Id);
        });
        todoService.deleteAll({
            format: 'json',
            Ids: deleteIds
        }, function () {
            var keepTodos = $.grep($scope.todos, function (todo) {
                return !todo.Completed;
            });
            $scope.todos = [];
            $scope.todos = keepTodos;
        });
    };
    $scope.getTodos();
};
//Default service injection doesn't work with minification, so a manual injection is necessary. The one liner injection
//TodoApp.controller('TodoListCtrl', ['$scope', '$location', 'todoService', 'loadingService', 'messageService', function ($scope, $location, todoService, loadingService, messageService) { ... }]);
//doesn't seem to work for me as I'd prefer this 
TodoListCtrl.$inject = ['$scope', '$location', 'todoService', 'messageService'];
var TodoEditCtrl = function ($scope, $routeParams, $location, todoService) {
    $scope.todo = todoService.get({ Id: $routeParams.todoId });
    
    $scope.update = function () {
        todoService.update({ Id: $scope.todo.Id }, $scope.todo, function () {
            $location.path('/');
        });
    };
};
TodoEditCtrl.$inject = ['$scope', '$routeParams', '$location', 'todoService'];

//Loading contoller
var LoadingCtrl = function ($scope, loadingService) {
    $scope.message = Application.properties.defaultMessage;
    $scope.$watch(function () { return loadingService.isLoading(); }, function (value) {
        $scope.message = loadingService.message;
        $scope.loading = value;
    });
};
LoadingCtrl.$inject = ['$scope', 'loadingService'];
var MessageCtrl = function ($scope, messageService) {       
    $scope.$watch(function () { return messageService.isShowing(); }, function (value) {
        $scope.Title = messageService.messageOptions.title;
        $scope.AlertType = messageService.messageOptions.alertType;
        $scope.Message = messageService.messageOptions.message;
        $scope.showMessage = value;
    });
    $scope.close = function() {
        messageService.closeMessage(); 
    };
};
MessageCtrl.$inject = ['$scope', 'messageService'];