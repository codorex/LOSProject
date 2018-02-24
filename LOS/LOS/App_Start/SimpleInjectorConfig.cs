using LOS.Bussiness;
using LOS.Bussiness.Entities.IdentityModels;
using LOS.Bussiness.Services.CartServices;
using LOS.Bussiness.Services.CategoryServices;
using LOS.Bussiness.Services.CommentServices;
using LOS.Bussiness.Services.ProductServices;
using LOS.Bussiness.Services.UserServices;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleInjector;
using SimpleInjector.Integration.Web;

namespace LOS.App_Start
{
    public static class SimpleInjectorConfig
	{
		public static Container Setup()
		{
			var container = new Container();
			container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

			container.Register<ApplicationContext, ApplicationContext>(Lifestyle.Scoped);
            container.Register<IUserStore<ApplicationUser>>(() => new UserStore<ApplicationUser>(container.GetInstance<ApplicationContext>()), Lifestyle.Scoped);
            container.Register(() => new UserManager<ApplicationUser>(container.GetInstance<IUserStore<ApplicationUser>>()), Lifestyle.Scoped);
			container.Register<IProductService, ProductService>(Lifestyle.Scoped);
			container.Register<ICategoryService, CategoryService>(Lifestyle.Scoped);
			container.Register<ICommentService, CommentService>(Lifestyle.Scoped);
			container.Register<ICartService, CartService>(Lifestyle.Scoped);

			container.Verify();
			return container;
		}
	}
}