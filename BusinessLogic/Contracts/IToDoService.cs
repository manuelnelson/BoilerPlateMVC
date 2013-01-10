using Application.DataInterface;
using Application.Models;

namespace Application.BusinessLogic.Contracts
{
    public interface IToDoService : IService<IToDoRepository, ToDo>
    {
        object GetRecent();
    }
}
