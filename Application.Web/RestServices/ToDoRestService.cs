using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using Application.BusinessLogic.Contracts;
using Application.Models;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace Application.Web.RestServices
{
    public class ToDoRestService
    {
        //REST Resource DTO
        [Route("/todos")]
        [Route("/todos/{Ids}")]
        public class TodoListDto : IReturn<List<TodoDto>>
        {
            public long[] Ids { get; set; }
            public TodoListDto(params long[] ids)
            {
                Ids = ids;
            }
        }

        [Route("/todos", "POST")]
        [Route("/todos/{Id}", "PUT")]
        [Route("/todos/{Id}", "GET")]
        public class TodoDto : IReturn<TodoDto>
        {
            public long Id { get; set; }
            public string Task { get; set; }
            public bool Completed { get; set; }
        }

        public class TodosService : Service
        {
            public IToDoService ToDoService { get; set; }  //Injected by IOC

            public object Get(TodoDto request)
            {
                return ToDoService.Get(request.Id);
            }

            public object Get(TodoListDto request)
            {
                return ToDoService.GetRecent();
            }

            public object Post(TodoDto request)
            {
                var toDoEntity = request.TranslateTo<ToDo>();
                ToDoService.Add(toDoEntity);
                return toDoEntity;
            }

            public object Put(TodoDto request)
            {
                var toDoEntity = request.TranslateTo<ToDo>();
                ToDoService.Update(toDoEntity);
                return toDoEntity;
            }

            public void Delete(TodoListDto request)
            { 
                throw new HttpError(HttpStatusCode.BadRequest, "Dude...you don't belong here");
                //ToDoService.DeleteAll(request.Ids);
            }

            public void Delete(TodoDto request)
            {
                var toDoEntity = request.TranslateTo<ToDo>();
                ToDoService.Delete(toDoEntity);
            }
        }

    }

}
