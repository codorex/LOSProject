﻿using System.Security.Claims;
using System.Security.Principal;

namespace LOS.IdentityModels.Domain
{
    public static class GenericIdentityExtensions
    {
        public static string FirstName(this IPrincipal user)
        {
            if (user.Identity.IsAuthenticated)
            {
                ClaimsIdentity identity = user.Identity as ClaimsIdentity;

                foreach (var claim in identity.Claims)
                {
                    if (claim.Type == "FirstName")
                    {
                        return claim.Value;
                    }
                }
                return "";
            }
            else
            {
                return "";
            }
        }

        public static string FullName(this IPrincipal user)
        {
            if (user.Identity.IsAuthenticated)
            {
                ClaimsIdentity identity = user.Identity as ClaimsIdentity;

                foreach (var claim in identity.Claims)
                {
                    if (claim.Type == "FullName")
                    {
                        return claim.Value;
                    }
                }
                return "";
            }
            else
            {
                return "";
            }
        }

        public static string Role(this IPrincipal user)
        {
            if (user.Identity.IsAuthenticated)
            {
                ClaimsIdentity identity = user.Identity as ClaimsIdentity;

                foreach (var claim in identity.Claims)
                {
                    if (claim.Type == "Role")
                    {
                        return claim.Value;
                    }
                }
                return "";
            }
            else
            {
                return "";
            }
        }
    }
}
