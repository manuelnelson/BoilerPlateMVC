using Application.DataInterface;
using Application.Models;
using ServiceStack.OrmLite;

namespace Application.DataContext.OrmLiteRepositories
{
    public class TestOrmLiteRepository : OrmLiteRepository<Test>, ITestRepository
    {
        public TestOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
