using System;
using Application.BusinessLogic.Contracts;
using Application.DataContext.Repositories;
using Application.DataInterface;
using Application.Models;
using Elmah;

namespace Application.BusinessLogic
{
    public class ToDoService : Service<IToDoRepository, ToDo>, IToDoService
    {
        private IToDoRepository ToDoRepository { get; set; }
        public ToDoService(IToDoRepository repository) : base(repository)
        {
            ToDoRepository = repository;
        }

        public object GetRecent()
        {
            try
            {
                return ToDoRepository.GetRecent();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to get latest To-dos", ex);
            }
        }
    }
}
