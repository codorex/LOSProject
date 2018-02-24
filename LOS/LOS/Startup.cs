using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Microsoft.Owin.Infrastructure;

namespace LOS
{
	public class Startup
	{
		public const string AuthenticationType = "ApplicationCookie";

		public void Configuration(IAppBuilder app)
		{
			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = AuthenticationType,
				LoginPath = new PathString("/Account/Login"),
				CookieName = "authentication_cookie"
			});
        }
	}
}
