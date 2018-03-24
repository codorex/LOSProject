using LOS.CartService.Domain;
using LOS.CategoryService.Domain;
using LOS.CommentService.Domain;
using LOS.DatabaseContext;
using LOS.ProducModel.Domain;
using LOS.ProductService.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleInjector;
using SimpleInjector.Integration.Web;

namespace LOS.WebApplication.App_Start
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
            container.Register<IProductService, LOS.ProductService.Domain.ProductService>(Lifestyle.Scoped);
            container.Register<ICategoryService, LOS.CategoryService.Domain.CategoryService>(Lifestyle.Scoped);
            container.Register<ICommentService, LOS.CommentService.Domain.CommentService>(Lifestyle.Scoped);
            container.Register<ICartService, LOS.CartService.Domain.CartService>(Lifestyle.Scoped);

            container.Verify();
            return container;
        }
    }
}