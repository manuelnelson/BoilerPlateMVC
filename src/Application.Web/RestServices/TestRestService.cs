using System.Net;
using Application.BusinessLogic.Contracts;
using Application.Models;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace Application.Web.RestServices
{
    public class TestRestService
    {
        [Route("/Tests", "POST")]
        [Route("/Tests", "PUT")]
        [Route("/Tests", "GET")]
        [Route("/Tests", "DELETE")]
        [Route("/Tests")]
        [Route("/Tests/{Id}")]
        public class TestDto : IReturn<TestDto>
        {
            public long Id { get; set; }
            public long[] Ids { get; set; }
        }

        public class TestsService : Service
        {
            public ITestService TestService { get; set; } //Injected by IOC

            public object Get(TestDto request)
            {
                if (request.Ids != null && request.Ids.Length > 0)
                    return TestService.Get(request.Ids);
                if (request.Id > 0)
                    return TestService.Get(request.Id);
                throw new HttpError(HttpStatusCode.BadRequest, "Invalid argument(s) supplied.");
            }

            public object Put(TestDto request)
            {
                var testEntity = request.TranslateTo<Test>();
                TestService.Update(testEntity);
                return testEntity;
            }

            public object Post(TestDto request)
            {
                var testEntity = request.TranslateTo<Test>();
                TestService.Add(testEntity);
                return testEntity;
            }

            public void Delete(TestDto request)
            {
                if (request.Ids != null && request.Ids.Length > 0)
                    TestService.DeleteAll(request.Ids);
                else
                    TestService.Delete(request.Id);
            }
        }

    }

}
