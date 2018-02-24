using Microsoft.AspNet.Identity;
using LOS.Bussiness.Entities.IdentityModels;

namespace LOS.Bussiness.Services.UserServices
{
	public class UserService : UserManager<ApplicationUser>, IUserService
	{
		private IUserStore<ApplicationUser> store;

		public UserService(IUserStore<ApplicationUser> store) : base(store)
		{
			this.store = store;
		}
	}
}
