using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace GesDoc.Web
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {

            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // otimizando carregamento de scripts
            BundleTable.EnableOptimizations = true;

            Application["ContadorAcessos"] = 0;

        }
        protected void Session_Start(object sender, EventArgs e)
        {
            Application["ContadorAcessos"] = (int)(Application["ContadorAcessos"]) + 1;
        }
        protected void Session_End(Object sender, EventArgs e)
        {
            Application["ContadorAcessos"] = (int)(Application["ContadorAcessos"]) - 1;
        }
    }
}