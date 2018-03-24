using LOS.DatabaseContext;
using System;
using System.Linq;
using System.Web;

namespace LOS
{
    /// <summary>
    /// Handles incoming images from the request, reads binary and caches them.
    /// </summary>
    public class ImageHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            Int32 fileId;
            if (context.Request.QueryString["id"] != null)
                fileId = Convert.ToInt32(context.Request.QueryString["id"]);
            else
                throw new ArgumentException("No parameter specified");

            context.Response.ContentType = "image/png";
            context.Response.ContentType = "image/JPEG";

            byte[] file = null;

            using (ApplicationContext db = new ApplicationContext())
            {
                file = db.Files.FirstOrDefault(f => f.FileID == fileId).Content;
            }

            TimeSpan cacheTime = new TimeSpan(1, 0, 0);
            context.Response.Cache.VaryByParams["*"] = true;
            context.Response.Cache.SetExpires(DateTime.Now.Add(cacheTime));
            context.Response.Cache.SetMaxAge(cacheTime);
            context.Response.Cache.SetCacheability(HttpCacheability.Public);
            context.Response.BinaryWrite(file);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}