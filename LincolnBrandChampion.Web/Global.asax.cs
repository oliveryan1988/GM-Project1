using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using LincolnBrandChampion.Web.Controllers;
using LincolnBrandChampion.Web.Helpers;

namespace LincolnBrandChampion.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_Error()
        {
            var exception = Server.GetLastError();
            var httpException = exception as HttpException;

            Response.Clear();
            Server.ClearError();

            var routeData = new RouteData();
            routeData.Values["controller"] = "HttpErrors";
            routeData.Values["action"] = "General";
            routeData.Values["exception"] = exception;
            Response.StatusCode = 500;
            if (httpException != null)
            {
                Response.StatusCode = httpException.GetHttpCode();
                switch (Response.StatusCode)
                {
                    case 404:
                        routeData.Values["action"] = "Http404";
                        break;
                    default:
                        routeData.Values["action"] = "ApplicationError";
                        break;
                }
            }
            else
            {
                routeData.Values["action"] = "ApplicationError";
            }

            if (httpException == null)
            {
                routeData.Values["action"] = "ApplicationError";
                EmailHelper.SendErrorEMail(exception, System.Web.HttpContext.Current.Session["userId"] != null ? System.Web.HttpContext.Current.Session["w_user"].ToString() : string.Empty);
            }

            // Avoid IIS7 getting in the middle
            Response.TrySkipIisCustomErrors = true;
            IController errorsController = new HttpErrorsController();
            HttpContextWrapper wrapper = new HttpContextWrapper(Context);
            var rc = new RequestContext(wrapper, routeData);
            errorsController.Execute(rc);

        }
    }
}
