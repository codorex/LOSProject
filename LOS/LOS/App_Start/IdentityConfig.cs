using LOS.Bussiness.Entities;
using LOS.Bussiness.Entities.IdentityModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LOS.App_Start
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