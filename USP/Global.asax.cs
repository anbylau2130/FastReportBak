using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using USP.Controllers;
using USP.Ioc;
using USP.Utility;


namespace USP
{
    public class MvcApplication : System.Web.HttpApplication

    {

        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(@Server.MapPath("App_Data/log4net.config")));
            ControllerBuilder.Current.SetControllerFactory(new UnityControllerFactory(Server.MapPath("App_Data/unity.config")));
            LogUtil.Info("SystemLogger", "Application_Start");
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            EasuiUiBundleConfig.RegisterBundles();
            UspBundleConfig.RegisterBundles();
           // RouteTable.Routes.RouteExistingFiles = false;
        }

        protected void Application_End(object sender, EventArgs e)
        {
            LogUtil.Info("SystemLogger", "Application_End");
        }

        /*
        protected void Application_Error(object sender, EventArgs e)
        {
            HttpException exception = Server.GetLastError().GetBaseException() as HttpException;
            LogUtil.Exception("SystemLogger", exception);
            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "Error");
            if (exception != null)
            {
                
                switch (exception.GetHttpCode())
                {
                    case 401:
                        // page not found
                        routeData.Values.Add("action", "Http401");
                        break;
                    case 404:
                        // page not found
                        routeData.Values.Add("action", "Http404");
                        break;
                    case 500:
                        // server error
                        routeData.Values.Add("action", "Http500");
                        break;
                    default:
                        routeData.Values.Add("action", "General");
                        break;
                }
            }
            else
            {
                routeData.Values.Add("action", "General");
            }
            Server.ClearError();
            IController errorController = new ErrorController();
            errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
        }
        */
    }
}
