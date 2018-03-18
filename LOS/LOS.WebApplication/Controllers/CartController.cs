using LOS.Bussiness.Services.CartServices;
using LOS.Bussiness.Services.CategoryServices;
using LOS.Bussiness.Services.ProductServices;
using LOS.Domain.Models.Entities;
using LOS.Models;
using LOS.WebApplication.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LOS.Controllers
{
    public class CartController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;
        private readonly ICartService cartService;

        public CartController(ICategoryService categoryService, IProductService productService, ICartService cartService)
        {
            this.categoryService = categoryService;
            this.productService = productService;
            this.cartService = cartService;
        }

        [AllowAnonymous]
        public async Task<ActionResult> ManageCart()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home", null);
            }

			List<Product> cartItems = (await cartService.GetCartAsync(User.Identity.GetUserId()));
			List<Product> distinctItems = cartItems.Distinct().ToList();

			for (int i = 0; i < distinctItems.Count; i++)
			{
				distinctItems[i].Images = await productService.GetImagesAsync(distinctItems[i].ProductID);
			}

			var cartItemsMapping = new Dictionary<int, List<Product>>();

			foreach (var item in cartItems)
			{
				if (cartItemsMapping.ContainsKey(item.ProductID) == false)
				{
					cartItemsMapping.Add(item.ProductID, new List<Product>());
				}

				cartItemsMapping[item.ProductID].Add(item);
			}

			var model = new CartViewModel() { CartItems = cartItemsMapping };

            return View(model);
        }

        public async Task Add(int productId)
        {
            await cartService.AddAsync(User.Identity.GetUserId(), productId);
        }

        public async Task Remove(int productId)
        {
            await cartService.RemoveAsync(User.Identity.GetUserId(), productId);
        }

        public async Task Clear()
        {
            await cartService.ClearAsync(User.Identity.GetUserId());
        }

        public PartialViewResult _Navbar()
        {
            List<Category> categories = Task.Run(async () =>
            {
                categories = await categoryService.GetAllAsync(); return categories;
            }).Result;

            int cartItemCount = Task.Run(async () =>
            {
                return (await cartService.GetCartAsync(User.Identity.GetUserId())).Count;
            }).Result;

            NavbarModel model = new NavbarModel
            {
                Categories = categories,
                CartItemCount = cartItemCount
            };

            return PartialView("_Navbar", model);
        }
    }
}