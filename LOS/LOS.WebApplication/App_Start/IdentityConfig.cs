﻿using LOS.Domain.Models.Entities.IdentityModels;
using Microsoft.AspNet.Identity;

namespace LOS.WebApplication.App_Start
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store) { }
    }

    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, string> store) : base(store) { }
    }
}