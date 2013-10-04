using System;
using System.Globalization;
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
        [Route("/todos/{Id}", "GET")]
        [Route("/todos/{Id}", "PUT")]
        [Route("/todos", "GET")]
        [Route("/todos", "PUT")]
        [Route("/todos", "POST")]
        [Route("/todos", "DELETE")]
        public class TodoDto : IReturn<TodoDto>
        {
            public long Id { get; set; }
            public long[] Ids { get; set; }
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
                if (request.Ids != null && request.Ids.Length > 0)
                    return ToDoService.Get(request.Ids);
                //store in cache
                if (request.Id > 0)
                {
                    var expireInTimespan = new TimeSpan(0, 0, 30);//cache for 30 seconds
                    var cacheKey = UrnId.CreateWithParts<TodoDto>(request.Id.ToString(CultureInfo.InvariantCulture));
                    return RequestContext.ToOptimizedResultUsingCache(CacheClient, cacheKey, expireInTimespan, () => ToDoService.Get(request.Id));
                }
                return ToDoService.GetRecent();
            }

            public object Post(TodoDto request)
            {
                var toDoEntity = request.TranslateTo<ToDo>();
                ToDoService.Add(toDoEntity);

                //Remove cache
                var cacheKey = UrnId.CreateWithParts<ToDo>();
                RequestContext.RemoveFromCache(CacheClient, cacheKey);
                return toDoEntity;
            }

            public object Put(TodoDto request)
            {
                var toDoEntity = request.TranslateTo<ToDo>();
                ToDoService.Update(toDoEntity);

                //Remove cache
                var cacheKey = UrnId.CreateWithParts<TodoDto>(request.Id.ToString(CultureInfo.InvariantCulture));
                RequestContext.RemoveFromCache(CacheClient, cacheKey);
                return toDoEntity;
            }

            public void Delete(TodoDto request)
            {
                if (request.Ids != null && request.Ids.Length > 0)
                    ToDoService.DeleteAll(request.Ids);
                else
                {
                    var cacheKey = UrnId.CreateWithParts<TodoDto>(request.Id.ToString());
                    RequestContext.RemoveFromCache(CacheClient, cacheKey);
                    ToDoService.Delete(request.Id);
                }

            }
        }

    }

}
