var TodoListCtrl = ['$scope', '$routeParams', '$location', 'todoService',
    function ($scope, $routeParams, $location, todoService ) {
    $scope.getTodos = function () {
        todoService.query({ format: 'json' }, function (todos) {
            $scope.todos = [];
            $scope.todos = $scope.todos.concat(todos);
        });
    };

    $scope.addTodo = function () {
        todoService.save(null, $scope.todo, function (task) {
            $scope.todos.push(task);
            $scope.todoForm.$setPristine();
            $scope.todo.Task = '';
        });
    };

    $scope.archive = function () {
        var deleteTodos = _.reject($scope.todos, function (todo) {
            return !todo.Completed;
        });

        var deleteIds = [];
        _.each(deleteTodos, function(item) {
            deleteIds.push(item.Id);
        });
        
        todoService.deleteAll({Ids: deleteIds}, function () {
            var keepTodos = _.reject($scope.todos, function (todo) {
                return todo.Completed;
            });
            $scope.todos = [];
            $scope.todos = keepTodos;
        });
    };
    $scope.getTodos();
}];