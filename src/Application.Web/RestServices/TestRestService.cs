using System.Collections.Generic;
using Application.BusinessLogic.Contracts;
using Application.Models;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace Application.Web.RestServices
{
    public class TestRestService
    {
        //REST Resource DTO
        [Route("/Tests")]
        [Route("/Tests/{Ids}")]
        public class TestListDto : IReturn<List<TestDto>>
        {
            public long[] Ids { get; set; }

            public TestListDto(params long[] ids)
            {
                Ids = ids;
            }
        }

        [Route("/Tests", "POST")]
        [Route("/Tests/{Id}", "PUT")]
        [Route("/Tests/{Id}", "GET")]
        public class TestDto : IReturn<TestDto>
        {
            public long Id { get; set; }
        }

        public class TestsService : Service
        {
            public ITestService TestService { get; set; } //Injected by IOC

            public object Get(TestDto request)
            {
                return TestService.Get(request.Id);
            }

            public object Get(TestListDto request)
            {
                //TODO Do something more interested.  Add query possibly 
                return TestService.GetFiltered(t => t.Id != 0);
            }

            public object Post(TestDto request)
            {
                var TestEntity = request.TranslateTo<Test>();
                TestService.Add(TestEntity);
                return TestEntity;
            }

            public object Put(TestDto request)
            {
                var TestEntity = request.TranslateTo<Test>();
                TestService.Update(TestEntity);
                return TestEntity;
            }

            public void Delete(TestListDto request)
            {
                TestService.DeleteAll(request.Ids);
            }

            public void Delete(TestDto request)
            {
                var TestEntity = request.TranslateTo<Test>();
                TestService.Delete(TestEntity);
            }
        }

    }

}
