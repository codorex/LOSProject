using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using System.Net;
using System.Web;
using Microsoft.AspNet.Identity.Owin;

namespace LOS.Common
{
	public static class IdentityExtensions
	{
		public static void AddOrUpdateClaim(this ClaimsIdentity identity, string key, string value)
		{
			if (identity == null || String.IsNullOrEmpty(key) || String.IsNullOrEmpty(value))
			{
				throw new ArgumentNullException();
			}

			Claim existingClaim = identity.FindFirst(key);

			if (existingClaim != null && String.IsNullOrEmpty(existingClaim.Value) == false)
			{
				identity.RemoveClaim(existingClaim);
			}

			identity.AddClaim(new Claim(key, value));

			IAuthenticationManager authenticationManager = HttpContext.Current.GetOwinContext().Authentication as IAuthenticationManager;
			authenticationManager.AuthenticationResponseGrant = new AuthenticationResponseGrant(new ClaimsPrincipal(identity), new AuthenticationProperties() { IsPersistent = true });
		}

		public static ClaimsIdentity AddOrUpdateClaimsCollection(this ClaimsIdentity identity, Dictionary<string, string> claimTypeValuesCollection)
		{
			if (claimTypeValuesCollection == null || identity == null)
			{
				throw new ArgumentOutOfRangeException();
			}

			foreach (var keyValuePair in claimTypeValuesCollection)
			{
				identity.AddOrUpdateClaim(keyValuePair.Key, keyValuePair.Value);
			}

			return identity;
		}
	}
}
