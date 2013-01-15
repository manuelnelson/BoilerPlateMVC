using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Configuration = Application.DataContext.Migrations.Configuration;

namespace Application.Web.App_Start
{
    public class EfConfigure
    {
        public static void Initialize()
        {
            Database.DefaultConnectionFactory = new SqlConnectionFactory(ConfigurationManager.ConnectionStrings["DataContext"].ConnectionString);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext.DataContext, Configuration>());
        }
    }
}