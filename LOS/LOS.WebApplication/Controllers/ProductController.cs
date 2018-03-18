using LOS.Bussiness.Services;
using LOS.Bussiness.Services.CartServices;
using LOS.Bussiness.Services.CategoryServices;
using LOS.Bussiness.Services.CommentServices;
using LOS.Bussiness.Services.ProductServices;
using LOS.Domain.Models.Entities;
using LOS.Domain.Models.Entities.IdentityModels;
using LOS.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LOS.Controllers
{
    public class ProductController : Controller
    {
        private IProductService productService;
        private ICategoryService categoryService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICommentService commentService;
        private readonly ICartService cartService;
        private readonly log4net.ILog log = log4net.LogManager.GetLogger("ProductController.cs");

        public ProductController(IProductService productService, ICategoryService categoryService, UserManager<ApplicationUser> userManager, ICommentService commentService, ICartService cartService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.userManager = userManager;
            this.commentService = commentService;
            this.cartService = cartService;
        }

        [HttpGet, AllowAnonymous]
        public async Task<ActionResult> Shop(string keyword, string sort = "", int id = 2, int page = 1)
        {
            return View(await GetModel(keyword, sort, id, page));
        }

        [HttpGet, AllowAnonymous]
        public async Task<ActionResult> Index(string keyword, string sort = "", int id = 2, int page = 1)
        {
            return View(await GetModel(keyword, sort, id, page));
        }

        [HttpPost, AllowAnonymous, ActionName("Index")]
        public async Task<ActionResult> IndexPost(string keyword, string sort = "", int id = 2, int page = 1)
        {
            DisplayModel model = await GetModel(keyword, sort, id, page);
            return View(model);
        }

        private async Task<DisplayModel> GetModel(string keyword, string sort = "", int id = 2, int page = 1)
        {
            int pageCount = 0;
            int pageSize = 9;

            Category category = await categoryService.GetAsync(id);
            PagedList<Product> productsPaged = null;
            int cartItemCount = (await cartService.GetCartAsync(User.Identity.GetUserId())).Count;

            if (category.ParentCategoryID == null)
            {
                //  get products for parent category AND thier children categories
                productsPaged = await productService.GetListByParentCategoryIdAsync(page, pageSize, id, keyword, sort);
            }
            else // or
            {
                //  get products for particular category
                productsPaged = await productService.GetListByCategoryIdAsync(page, pageSize, id, keyword, sort);
            }

            // calculate the total number of pages
            if (productsPaged.TotalCount % pageSize == 0)
            {
                pageCount = productsPaged.TotalCount / pageSize;
            }
            else
            {
                pageCount = (productsPaged.TotalCount / pageSize) + 1;
            }

            // get thumbnail image for the product which will be displayed in the product list
            var images = new List<Image>();

            foreach (var product in productsPaged.Collection)
            {
                images.Add((await productService.GetImagesAsync(product.ProductID)).FirstOrDefault(i => i.ProductID == product.ProductID));
            }

            return new DisplayModel { ProductsPaged = productsPaged, PageCount = pageCount, PageNumber = page, Category = category, keyword = keyword, sort = sort, CartItemCount = cartItemCount, ProductsImages = images };
        }

        [AllowAnonymous]
        public PartialViewResult _Navbar()
        {
            int cartItemCount = Task.Run(async () => { return (await cartService.GetCartAsync(User.Identity.GetUserId())).Count; }).Result;
            List<Category> categories = Task.Run(async () =>
            {
                categories = await categoryService.GetAllAsync(); return categories;
            }).Result;

            NavbarModel model = new NavbarModel
            {
                Categories = categories,
                CartItemCount = cartItemCount
            };

            return PartialView("_Navbar", model);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetPage(int id, int pageNumber)
        {
            int pageCount = 1;
            int pageSize = 9;

            List<Category> categories = await categoryService.GetAllAsync();
            Category categoryFilter = categories.SingleOrDefault(c => c.CategoryID == id);

            PagedList<Product> productsPaged = null;

            if (categoryFilter.ParentCategoryID == null)
            {
                productsPaged = await productService.GetListByParentCategoryIdAsync(pageNumber, pageSize, id);
            }
            else
            {
                productsPaged = await productService.GetListByCategoryIdAsync(pageNumber, pageSize, id);
            }

            if (productsPaged.TotalCount % pageSize == 0)
            {
                pageCount = productsPaged.TotalCount / pageSize;
            }
            else
            {
                pageCount = (productsPaged.TotalCount / pageSize) + 1;
            }

            var model = new DisplayModel { ProductsPaged = productsPaged, PageCount = pageCount, PageNumber = pageNumber, Category = await categoryService.GetAsync(id) };
            return PartialView(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> Details(int id)
        {
            Product product = await productService.GetAsync(id);
            product.Rating = await productService.GetRatingAsync(id);

            List<Comment> comments = await commentService.GetByProductId(id);

            int commentsCount = comments.Count;
            int ratingVoteCount = await productService.GetVoteCountAsync(id);
            int cartItemCount = (await cartService.GetCartAsync(User.Identity.GetUserId())).Count;
            bool userHasVoted = await productService.CheckIfUserHasVotedAsync(User.Identity.GetUserId(), id);

            Category category = await categoryService.GetAsync(product.CategoryID);
            List<Image> images = await productService.GetImagesAsync(id);

            var model = new DetailsModel
            {
                ProductID = product.ProductID,
                Name = product.Name,
                CategoryID = product.CategoryID,
                DateReleased = product.DateReleased,
                Description = product.Description,
                Price = product.Price,
                CommentsCount = commentsCount,
                Rating = product.Rating,
                RatingVoteCount = ratingVoteCount,
                UserHasVoted = userHasVoted,
                Quantity = product.Quantity,
                CartItemCount = cartItemCount,
                Category = category,
                Images = images
            };

            return View(model);
        }

        [AllowAnonymous]
        public PartialViewResult _Comments(int productId)
        {
            List<Comment> comments = Task.Run(async () =>
            {
                comments = await commentService.GetByProductId(productId);
                return comments;
            }).Result;

            List<ApplicationUser> users = new List<ApplicationUser>();
            string currentUserId = User.Identity.GetUserId();

            foreach (var comment in comments)
            {
                ApplicationUser user;
                user = (Task.Run(async () => { user = await userManager.Users.SingleOrDefaultAsync(u => u.Id == comment.UserID); return user; })).Result;
                users.Add(user);
            }

            return PartialView(new CommentsModel { Comments = comments, Users = users, productId = productId, CurrentUserId = currentUserId });
        }

        [HttpPost]
        public async Task AddComment(Comment comment)
        {
            if (comment.UserID != null && comment.Content != null)
            {
                comment.CommentID = 1;
                await commentService.CreateAsync(comment);
                log.Info("Comment added to product with ID: " + comment.ProductID + " by user: " + (await userManager.FindByIdAsync(comment.UserID)).UserName);
            }
        }

        [HttpPost]
        public async Task DeleteComment(int commentId)
        {
            Comment comment = await commentService.GetAsync(commentId);

            if (User.IsInRole("Moderator") || User.Role() == "Admin" || User.Identity.GetUserId() == comment.UserID)
            {
                await commentService.DeleteAsync(commentId);
                log.Info("Comment deleted from product with ID: " + comment.ProductID);
            }
        }

        [HttpPost]
        public async Task Rate(int productId, int rating)
        {
            await productService.RateAsync(productId, rating, User.Identity.GetUserId());

            Product product = await productService.GetAsync(productId);
            product.Rating = await productService.GetRatingAsync(productId);

            await productService.UpdateAsync(product);
        }

        [HttpPost]
        public async Task<ActionResult> UploadImages(HttpPostedFileBase file, int productId)
        {
            if (file.ContentType != null && SUPPORTED_IMAGE_TYPES.Contains(file.ContentType.Substring(file.FileName.IndexOf('.'))))
            {
                byte[] buffer = new byte[file.ContentLength];
                await file.InputStream.ReadAsync(buffer, 0, file.ContentLength);

                var newFile = new File { Content = buffer, ContentType = file.ContentType, Length = file.ContentLength, Name = file.FileName };
                await productService.UploadFileAsync(newFile);

                newFile = await productService.GetFileByNameAsync(newFile.Name);
                await productService.AddImageAsync(new Image { Name = newFile.Name, ProductID = productId, FileID = newFile.FileID });
            }

            return RedirectToAction("Details", "Product", new { id = productId });
        }

        private readonly string[] SUPPORTED_IMAGE_TYPES = new string[]
        {
            "jpg",
            "png",
            "gif"
        };
    }
}