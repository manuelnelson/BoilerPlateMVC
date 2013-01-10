using Application.DataInterface;
using Application.Models;

namespace Application.DataContext.Repositories
{
    public class TestRepository : Repository<Test>, ITestRepository
    {
        public TestRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}