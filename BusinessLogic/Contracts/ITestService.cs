using Application.DataInterface;
using Application.Models;

namespace Application.BusinessLogic.Contracts
{
    public interface ITestService : IService<ITestRepository, Test>
    {
    }
}
