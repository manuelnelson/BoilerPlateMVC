using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Application.Web.App_Start;
using ServiceStack.MiniProfiler;

namespace Application.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        
        protected void Application_BeginRequest()
        {
            if (Request.IsLocal)
                Profiler.Start();
        }

        protected void Application_EndRequest()
        {
            Profiler.Stop();
        }
    }
}