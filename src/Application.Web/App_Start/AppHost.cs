using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Web.Mvc;
using Application.BusinessLogic;
using Application.BusinessLogic.Contracts;
using Application.DataContext.OrmLiteRepositories;
using Application.DataContext.Repositories;
using Application.DataInterface;
using Application.Web.App_Start;
using Application.Web.RestServices;
using Funq;
using ServiceStack.Configuration;
using ServiceStack.Logging;
using ServiceStack.Logging.Elmah;
using ServiceStack.Logging.Support.Logging;
using ServiceStack.MiniProfiler;
using ServiceStack.MiniProfiler.Data;
using ServiceStack.Mvc;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.WebHost.Endpoints;
using IDbConnectionFactory = ServiceStack.OrmLite.IDbConnectionFactory;

[assembly: WebActivator.PreApplicationStartMethod(typeof(AppHost), "Start")]
/**
 * Entire ServiceStack Starter Template configured with a 'Hello' Web Service and a 'Todo' Rest Service.
 *
 * Auto-Generated Metadata API page at: /metadata
 * See other complete web service examples at: https://github.com/ServiceStack/ServiceStack.Examples
 */

namespace Application.Web.App_Start
{
	//A customizeable typed UserSession that can be extended with your own properties
	//To access ServiceStack's Session, Cache, etc from MVC Controllers inherit from ControllerBase<CustomUserSession>
	public class CustomUserSession : AuthUserSession
	{
		public string CustomProperty { get; set; }
	}

	public class AppHost : AppHostBase
	{		
		public AppHost() //Tell ServiceStack the name and where to find your web services
			: base("Application Rest Service", typeof(ToDoRestService).Assembly) { }

		public override void Configure(Container container)
		{
			//Set JSON web services to return idiomatic JSON camelCase properties
			ServiceStack.Text.JsConfig.EmitCamelCaseNames = false;
		
			//Uncomment to change the default ServiceStack configuration
			//SetConfig(new EndpointHostConfig {
			//});

            //Use Elmah with ServiceStack
            LogManager.LogFactory = new ElmahLogFactory(new NullLogFactory());

            RegisterDependencies(container);

            //Enable Authentication
			//ConfigureAuth(container);

			//Set MVC to use the same Funq IOC as ServiceStack
			ControllerBuilder.Current.SetControllerFactory(new FunqControllerFactory(container));
		}

	    private void RegisterDependencies(Container container)
	    {
            //Register your repositories
            //Make the default lifetime of objects limited to request
            container.DefaultReuse = ReuseScope.Request;
            var appSettings = new AppSettings();
            var connectionString = appSettings.Get("SQLSERVER_CONNECTION_STRING", //AppHarbor or Local connection string
                ConfigUtils.GetConnectionString("DataContext"));


            //---Entity Framework (Uncomment to use)
            //database
            //container.Register<IUnitOfWork>(c => new DataContext.DataContext());
            ////repositories
            //container.Register<IToDoRepository>(c => new ToDoRepository(c.Resolve<IUnitOfWork>()));
            //EfConfigure.Initialize(connectionString);
            ////services
            //container.Register<IToDoService>(c => new ToDoService(c.Resolve<IToDoRepository>() as ToDoRepository));

            //--OrmLite
            //database
            //repositories
            container.Register<IToDoRepository>(c => new ToDoOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            OrmLiteConfigure.Initialize(container, connectionString);
            //services
            container.Register<IToDoService>(c => new ToDoService(c.Resolve<IToDoRepository>() as ToDoOrmLiteRepository));

	    }

	    /* Uncomment to enable ServiceStack Authentication and CustomUserSession
		private void ConfigureAuth(Funq.Container container)
		{
			var appSettings = new AppSettings();

			//Default route: /auth/{provider}
			Plugins.Add(new AuthFeature(this, () => new CustomUserSession(),
				new IAuthProvider[] {
					new CredentialsAuthProvider(appSettings), 
					new FacebookAuthProvider(appSettings), 
					new TwitterAuthProvider(appSettings), 
					new BasicAuthProvider(appSettings), 
				})); 

			//Default route: /register
			Plugins.Add(new RegistrationFeature()); 

			//Requires ConnectionString configured in Web.Config
			var connectionString = ConfigurationManager.ConnectionStrings["AppDb"].ConnectionString;
			container.Register<IDbConnectionFactory>(c =>
				new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider));

			container.Register<IUserAuthRepository>(c =>
				new OrmLiteAuthRepository(c.Resolve<IDbConnectionFactory>()));

			var authRepo = (OrmLiteAuthRepository)container.Resolve<IUserAuthRepository>();
			authRepo.CreateMissingTables();
		}
		*/

		public static void Start()
		{
			new AppHost().Init();
		}
	}
}