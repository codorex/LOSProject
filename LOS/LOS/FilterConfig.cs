using LOS.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LOS
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
			filters.Add(new AuthorizeAttribute());
			filters.Add(new Log4NetExceptionHandler());
		}
	}
}