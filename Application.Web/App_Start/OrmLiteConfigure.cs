using Application.DataContext.OrmLiteRepositories;
using Application.DataInterface;
using Funq;

namespace Application.Web.App_Start
{
    public class OrmLiteConfigure
    {
        public static void Initialize(Container container)
        {
            var todoRepo = (ToDoOrmLiteRepository)container.Resolve<IToDoRepository>();
            todoRepo.CreateMissingTables();
        }
    }
}