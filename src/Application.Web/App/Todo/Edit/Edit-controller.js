var TodoEditCtrl = ['$scope', '$routeParams', '$location', 'todoService',
    function ($scope, $routeParams, $location, todoService) {
        $scope.todo = todoService.get({ Id: $routeParams.todoId });

        $scope.update = function () {
            todoService.update({ Id: $scope.todo.Id }, $scope.todo, function () {
                $location.path('/');
            });
        };

    }];