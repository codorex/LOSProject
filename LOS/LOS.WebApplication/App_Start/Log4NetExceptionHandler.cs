using System.Web.Mvc;

namespace LOS.WebApplication.App_Start
{
    public class Log4NetExceptionHandler : HandleErrorAttribute
    {
        private readonly log4net.ILog log = log4net.LogManager.GetLogger("Log4NetExceptionHandler.cs");

        public override void OnException(ExceptionContext filterContext)
        {
            while (filterContext.Exception.InnerException != null)
            {
                filterContext.Exception = filterContext.Exception.InnerException;
            }

            log.Error(filterContext.Exception.Source + ": " + filterContext.Exception.Message);
            base.OnException(filterContext);
        }
    }
}