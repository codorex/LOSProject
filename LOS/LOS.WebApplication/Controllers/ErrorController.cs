using LOS.Bussiness.Services.CategoryServices;
using LOS.Domain.Models.Entities;
using LOS.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LOS.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        private readonly ICategoryService categoryService;

        public ErrorController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public ActionResult NotFound()
        {
            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return View("NotFound");
        }

        public ActionResult InternalServerError()
        {
            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return View("InternalServerError");
        }

        public PartialViewResult _Navbar()
        {
            List<Category> categories = Task.Run(async () =>
            {
                categories = await categoryService.GetAllAsync(); return categories;
            }).Result;

            NavbarModel model = new NavbarModel
            {
                Categories = categories
            };

            return PartialView("_Navbar", model);
        }
    }
}