using LOS.Bussiness;
using LOS.Bussiness.Entities;
using LOS.Bussiness.Entities.IdentityModels;
using LOS.Bussiness.Services.CartServices;
using LOS.Bussiness.Services.ProductServices;
using LOS.Bussiness.Services.UserServices;
using LOS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LOS.Controllers
{
    public class HomeController : Controller
    {
		
		private readonly ApplicationContext context;
		private UserService userService;
		private readonly ICartService cartService;
		private readonly IProductService productService;

		public HomeController(ApplicationContext context, ICartService cartService, IProductService productService)
		{
			this.context = context;
			this.productService = productService;
			this.cartService = cartService;
			this.userService = new UserService(new UserStore<ApplicationUser>(context));
		}

        [HttpGet, AllowAnonymous]
        public async Task<ActionResult> Index()
        {
			List<Product> products = await productService.GetLatestAsync(4);
			List<Image> images = new List<Image>();

			foreach (var product in products)
			{
				images.Add((await productService.GetImagesAsync(product.ProductID)).FirstOrDefault(i => i.ProductID == product.ProductID));
			}

			var model = new LatestProductsModel { Products = products, Images = images };

			return View(model);
        }

		[AllowAnonymous]
		public PartialViewResult _Navbar()
		{
			int cartItemCount = Task.Run(async () => { return (await cartService.GetCartAsync(User.Identity.GetUserId())).Count; }).Result;
			NavbarModel model = new NavbarModel
			{
				Categories = context.Categories.ToList(),
				CartItemCount =cartItemCount
			};

			return PartialView("_Navbar", model);
		}
	}
}