using LOS.ProducModel.Domain;
using Microsoft.AspNet.Identity;

namespace LOS.UserService.Domain
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
