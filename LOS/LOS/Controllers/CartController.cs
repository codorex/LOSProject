using LOS.Bussiness.Services.CartServices;
using LOS.Bussiness.Services.CategoryServices;
using LOS.Bussiness.Services.ProductServices;
using LOS.Domain.Models.Entities;
using LOS.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
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
            else
            {
                return View(await cartService.GetCartAsync(User.Identity.GetUserId()));
            }
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