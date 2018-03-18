using LOS.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using LOS.Bussiness.Services.CommentServices;
using LOS.Bussiness.Services.CategoryServices;
using System;
using LOS.Bussiness.Services.ProductServices;
using LOS.Domain.Models.Entities.IdentityModels;
using LOS.Domain.Models.Entities;
using LOS.Common;
using LOS.Bussiness.Services.CartServices;
using LOS.WebApplication;
using LOS.EmailClient;

namespace LOS.Controllers
{
    public class AccountController : Controller
    {
        private readonly log4net.ILog log = log4net.LogManager.GetLogger("AccountController.cs");
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICommentService commentService;
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;
        private readonly ICartService cartService;

        public AccountController(UserManager<ApplicationUser> userManager, ICommentService commentService, ICategoryService categoryService, IProductService productService, ICartService cartService)
        {
            this.commentService = commentService;
            this.userManager = userManager;
            this.categoryService = categoryService;
            this.productService = productService;
            this.cartService = cartService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string username = "", string password = "")
        {
            return RedirectToAction("Index", "Home", null);
        }

        [HttpPost]
        [AllowAnonymous]
        [ActionName("Login")]
        public async Task<JsonResult> LoginPost(string username, string password)
        {
            ApplicationUser user = userManager.Users.SingleOrDefault(x => x.UserName == username);

            if (user != null && userManager.CheckPassword(user, password))
            {
                ClaimsIdentity userIdentity = await userManager.CreateIdentityAsync(user, Startup.AuthenticationType);
                userIdentity.AddClaim(new Claim("FirstName", user.FirstName));
                userIdentity.AddClaim(new Claim("FullName", user.FirstName + " " + user.LastName));
                userIdentity.AddClaim(new Claim("Role", user.Role));

                Request.GetOwinContext().Authentication.SignIn(userIdentity);

                return Json(new { message = 202 });
            }

            throw new ArgumentException("Login Failed! Invalid Credentials.");
        }

        [HttpPost]
        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut(Startup.AuthenticationType);

            return RedirectToAction("Index", "Home", null);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register(string returnUrl)
        {
            return View(new RegisterModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            var user = new ApplicationUser()
            {
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Role = "Regular"
            };

            if (await userManager.Users.AnyAsync(x => x.Email == user.Email))
            {
                ModelState.AddModelError("emailUsed", $"User with e-mail: {model.Email} already exists!");
                return View(model);
            }

            if (await userManager.Users.AnyAsync(x => x.UserName == user.UserName))
            {
                ModelState.AddModelError("usernameTaken", $"Username \"{model.Username}\" is taken.");
                return View(model);
            }

            IdentityResult rdyCheck = await userManager.CreateAsync(user, model.Password);

            if (rdyCheck.Succeeded)
            {
                await userManager.AddToRoleAsync(user.Id, "Regular");
                log.Info("Registered user: " + user.UserName + "\n Role: " + user.Role);
            }

            await LoginPost(user.UserName, model.Password);

            return Redirect(model.ReturnUrl);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Update(UpdateUserModel model)
        {
            List<Product> ratedProducts = await productService.GetRatedProductsForUserAsync(User.Identity.GetUserId());
            List<Product> reviewedProducts = await commentService.GetReviewedProductsForUserAsync(User.Identity.GetUserId());

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home", null);
            }
            else
            {
                string userId = Request.GetOwinContext().Authentication.User.Identity.GetUserId();
                ApplicationUser user = await userManager.Users.SingleOrDefaultAsync(x => x.Id == userId);

                int productReviews = await commentService.GetCommentsCountForUserAsync(User.Identity.GetUserId());

                model.Username = user.UserName;
                model.Email = user.Email;
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.ProductReviews = productReviews;
                model.RatedProducts = ratedProducts;
                model.ReviewedProducts = reviewedProducts;

                return View(model);
            }
        }

        [HttpPost, ActionName("Update")]
        [Authorize(Roles = "Admin, Regular, Moderator")]
        public async Task<ActionResult> UpdatePost(UpdateUserModel model)
        {
            string userId = Request.GetOwinContext().Authentication.User.Identity.GetUserId();

            ApplicationUser user = userManager.FindById(userId);
            user.UserName = model.Username;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;

            try
            {
                await userManager.UpdateAsync(user);
            }
            catch (Exception e)
            {
                log.Error("ERROR UPDATING ACCOUNT " + e.InnerException + e.Message);
            }

            var identity = User.Identity as ClaimsIdentity;

            Request.GetOwinContext().Authentication.SignOut(Startup.AuthenticationType);

            Dictionary<string, string> updatedClaimsValues = new Dictionary<string, string>();
            updatedClaimsValues.Add("FirstName", user.FirstName);
            updatedClaimsValues.Add("FullName", user.FirstName + " " + user.LastName);

            identity.AddOrUpdateClaimsCollection(updatedClaimsValues);

            Request.GetOwinContext().Authentication.SignIn(identity);

            CustomEmailClient.SendEmail(new System.Net.Mail.MailAddress(user.Email), "Account Changes", "There have been changes made to your account");

            return RedirectToAction("Index", "Home", null);
        }

        [AllowAnonymous]
        public PartialViewResult _Navbar()
        {
            int cartItemCount = Task.Run(async () => { return (await cartService.GetCartAsync(User.Identity.GetUserId())).Count; }).Result;

            List<Category> categories = Task.Run(async () =>
            {
                categories = await categoryService.GetAllAsync();
                return categories;
            }).Result;

            NavbarModel model = new NavbarModel
            {
                Categories = categories,
                CartItemCount = cartItemCount
            };

            return PartialView("_Navbar", model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Regular, Moderator")]
        private async Task<bool> CheckPassword(string password)
        {
            string userId = User.Identity.GetUserId();
            ApplicationUser user = await userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);
            return await userManager.CheckPasswordAsync(user, password);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ChangePassword()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home", null);
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin, Regular, Moderator")]
        [HttpPost, ActionName("ChangePassword")]
        public async Task<ActionResult> ChangePasswordPost(ChangePasswordModel model)
        {
            bool checkPass = await CheckPassword(model.OldPassword);

            if (checkPass)
            {
                await userManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("passwordChangeError", "Error. Password has not been changed!");
                return View("ChangePassword");
            }
        }
    }
}