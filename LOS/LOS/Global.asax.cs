using LOS.Bussiness.Entities;
using LOS.Bussiness;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using LOS.App_Start;
using System.Web.Security;

namespace LOS
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(SimpleInjectorConfig.Setup()));
        }

		protected void Application_AuthenticateRequest(object sender, EventArgs e)

		{
			string authCookie = FormsAuthentication.FormsCookieName;
			string path = HttpContext.Current.Request.Path;

			if (authCookie == null)
			{
				if (path.Contains("ChangePassword"))
				{
					Response.Redirect("Index/Home");
					Response.End();
					return;
				}
			}
		}
    }
}
