using System.Data.Entity;
using Application.DataContext.Migrations;

namespace Application.Web.App_Start
{
    public class EfConfigure
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext.DataContext, Configuration>());
        }
    }
}