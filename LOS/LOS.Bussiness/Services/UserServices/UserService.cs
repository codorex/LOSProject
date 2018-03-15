using LOS.Domain.Models.Entities.IdentityModels;
using Microsoft.AspNet.Identity;

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
