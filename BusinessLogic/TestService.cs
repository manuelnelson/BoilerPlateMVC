using Application.BusinessLogic.Contracts;
using Application.DataContext.Repositories;
using Application.DataInterface;
using Application.Models;

namespace Application.BusinessLogic
{
    public class TestService : Service<TestRepository, Test>, ITestService
    {
        private ITestRepository TestRepository { get; set; }

        public TestService(TestRepository repository) : base(repository)
        {
            TestRepository = repository;
        }
    }
}
