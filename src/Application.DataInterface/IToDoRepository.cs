using System.Collections.Generic;
using Application.Models;

namespace Application.DataInterface
{
    public interface IToDoRepository : IRepository<ToDo>
    {
        IEnumerable<ToDo> GetRecent();
    }
}
