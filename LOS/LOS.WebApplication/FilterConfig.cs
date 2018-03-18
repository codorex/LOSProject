using LOS.WebApplication.App_Start;
using System.Web.Mvc;

namespace LOS.WebApplication
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