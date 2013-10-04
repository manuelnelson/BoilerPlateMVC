//Todoservice
TodoApp.factory('todoService', ['$resource', function ($resource) {
    return $resource('/api/todos/:Id', { Id: '@Id' },
    {
        update: { method: 'PUT', params: { format: 'json' } },
        deleteAll: { method: 'DELETE', params: { format: 'json' } }
    });
}]);

