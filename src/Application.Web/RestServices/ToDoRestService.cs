using System;
using System.Collections.Generic;
using System.Linq;
using Application.BusinessLogic.Contracts;
using Application.Models;
using ServiceStack.CacheAccess;
using ServiceStack.Common;
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

        //Caching things I probably wouldn't but doing it as an example
        public class TodosService : Service
        {
            //Injected by IOC
            public IToDoService ToDoService { get; set; }  
            public ICacheClient CacheClient { get; set; }
            public object Get(TodoDto request)
            {
                var cacheKey = UrnId.CreateWithParts<TodoDto>(request.Id.ToString());
                var expireInTimespan = new TimeSpan(0, 0, 30);//cache for 30 seconds
                return RequestContext.ToOptimizedResultUsingCache(CacheClient, cacheKey, expireInTimespan, () => ToDoService.Get(request.Id));                
            }
            public object Get(TodoListDto request)
            {
                var cacheKey = UrnId.CreateWithParts<TodoListDto>();
                var expireInTimespan = new TimeSpan(0, 0, 30);//cache for 30 seconds
                return RequestContext.ToOptimizedResultUsingCache(CacheClient, cacheKey, expireInTimespan, () => ToDoService.GetRecent());                
            }

            public object Post(TodoDto request)
            {
                var toDoEntity = request.TranslateTo<ToDo>();
                ToDoService.Add(toDoEntity);

                //Manual cache update
                var cacheKey = UrnId.CreateWithParts<TodoListDto>();
                CacheClient.Replace(cacheKey, ToDoService.GetRecent());
                return toDoEntity;
            }

            public object Put(TodoDto request)
            {
                var toDoEntity = request.TranslateTo<ToDo>();
                ToDoService.Update(toDoEntity);
                
                //Replace cache
                var cacheKey = UrnId.CreateWithParts<TodoDto>(request.Id.ToString());
                CacheClient.Replace(cacheKey, toDoEntity);
                //Force update - cache invalidation NOO!!
                var cacheKeyRecent = UrnId.CreateWithParts<TodoListDto>();
                CacheClient.Remove(cacheKeyRecent);
                return toDoEntity;
            }

            public void Delete(TodoListDto request)
            {                 
                ToDoService.DeleteAll(request.Ids);

                var cacheKey = UrnId.CreateWithParts<TodoListDto>();
                CacheClient.Remove(cacheKey);

                //remove all cached get requests by id
                foreach (var cacheKeyId in request.Ids.Select(id => UrnId.CreateWithParts<TodoDto>(id.ToString())))
                {
                    CacheClient.Remove(cacheKeyId);
                }
            }

            public void Delete(TodoDto request)
            {
                var toDoEntity = request.TranslateTo<ToDo>();
                ToDoService.Delete(toDoEntity);

                //Force cache to update itself
                var cacheKey = UrnId.CreateWithParts<TodoDto>(request.Id.ToString());
                CacheClient.Remove(cacheKey);
            }
        }

    }

}
