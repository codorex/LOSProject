using LOS.CartService.Domain;
using LOS.DatabaseContext;
using LOS.WebApplication.Models;
using LOS.ProducModel.Domain;
using LOS.ProductService.Domain;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LOS.WebApplication.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationContext context;
        private readonly ICartService cartService;
        private readonly IProductService productService;

        public HomeController(ApplicationContext context, ICartService cartService, IProductService productService)
        {
            this.context = context;
            this.productService = productService;
            this.cartService = cartService;
        }

        [HttpGet, AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            List<Product> products = await productService.GetLatestAsync(4);
            List<LOS.ImageModel.Domain.Image> images = new List<LOS.ImageModel.Domain.Image>();

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
                CartItemCount = cartItemCount
            };

            return PartialView("_Navbar", model);
        }
    }
}