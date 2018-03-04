using LOS.Bussiness.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Web;

namespace LOS.Bussiness.Services.ProductServices
{
	public class ProductService : IProductService
	{
		private readonly ApplicationContext context;

		public ProductService(ApplicationContext context)
		{
			this.context = context;
		}

		public async Task CreateAsync(Product product)
		{
			context.Products.Add(product);

			await context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int productID)
		{
			Product product = await context.Products.SingleOrDefaultAsync(p => p.ProductID == productID);
			context.Products.Remove(product);

			await context.SaveChangesAsync();
		}

		public async Task<List<Product>> GetAllAsync()
		{
			return await context.Products.ToListAsync();
		}

		public async Task<Product> GetAsync(int productID)
		{
			Product product = await context.Products.SingleOrDefaultAsync(p => p.ProductID == productID);

			return product;
		}

		public async Task<PagedList<Product>> GetListAsync(int pageNumber, int pageSize, string search, string sort = "")
		{
			if (string.IsNullOrEmpty(search))
			{
				search = "";
			}

			IQueryable<Product> products = context.Products.Where(p => p.Name.Contains(search));
			PagedList<Product> productsPaged = null;

			return await productsPaged.SortAsync(products, sort, pageNumber, pageSize);
		}

		//Get Paged Products (sorted and filtered) for specific children category 
		public async Task<PagedList<Product>> GetListByCategoryIdAsync(int pageNumber, int pageSize, int categoryID, string keyword = "", string sort = "")
		{
			keyword = string.IsNullOrEmpty(keyword) ? "" : keyword.ToLower();
			IQueryable<Product> products = context.Products.Where(p => p.CategoryID == categoryID && p.Name.Contains(keyword));
			PagedList<Product> productsPaged = null;
			
			return await productsPaged.SortAsync(products, sort, pageNumber, pageSize);
		}

		//Get Paged Products (sorted and filtered) for all children categories
		/// <summary>
		/// Returns a paged collection of all products, that belong to all child categories of the selected category.
		/// </summary>
		/// <param name="pageNumber">The number of the current page.</param>
		/// <param name="pageSize">The amount of items, displayed on a single page.</param>
		/// <param name="categoryID">The ID of the category, by which the products will be filtered.</param>
		/// <param name="keyword">Product name filter.</param>
		/// <param name = "sort" > Determines the order in which the collection will be sorted.</param>
		/// <returns></returns>
		public async Task<PagedList<Product>> GetListByParentCategoryIdAsync(int pageNumber, int pageSize, int categoryID, string keyword = "", string sort = "")
		{
			// get a collection of all child categories of the current parent category
			List<Category> children = await context.Categories.Where(x => x.ParentCategoryID != null && x.ParentCategoryID.Value == categoryID).ToListAsync();
			List<int> childrenIDs = new List<int>();
			keyword = string.IsNullOrEmpty(keyword) ? "" : keyword.ToLower();

			foreach (var item in children)
			{
				childrenIDs.Add(item.CategoryID);
			}

			IQueryable<Product> products = context.Products.Where(p => childrenIDs.Contains(p.CategoryID) && p.Name.Contains(keyword));
			PagedList<Product> productsPaged = null;

			return await productsPaged.SortAsync(products, sort, pageNumber, pageSize);
		}

		public async Task UpdateAsync(Product product)
		{
			Product result = await context.Products.SingleOrDefaultAsync(p => p.ProductID == product.ProductID);
			result.CategoryID = product.CategoryID;
			result.DateReleased = product.DateReleased;
			result.Name = product.Name;
			result.Price = product.Price;

			await context.SaveChangesAsync();
		}

		public async Task RateAsync(int productId, int rating, string userId)
		{
			var result = new RatingLog { ProductID = productId, Rating = rating, UserID = userId };
			context.RatingLogs.Add(result);
			await context.SaveChangesAsync();
		}

		public async Task<decimal> GetRatingAsync(int productId)
		{
			List<RatingLog> productRatings = await context.RatingLogs.Where(r => r.ProductID == productId).ToListAsync();

			int ratingCount = productRatings.Count == 0 ? 1 : productRatings.Count;
			int overall = 0;

			foreach (var ratingLog in productRatings)
			{
				overall += ratingLog.Rating;
			}

			return overall / ratingCount;
		}

		public async Task<int> GetVoteCountAsync(int productId)
		{
			return (await context.RatingLogs.Where(l => l.ProductID == productId).ToListAsync()).Count;
		}

		public Task<bool> CheckIfUserHasVotedAsync(string userId, int productId)
		{
			return context.RatingLogs.AnyAsync(l => l.ProductID == productId && l.UserID == userId);
		}

		public async Task UpdateQuantityAsync(int productId)
		{
			Product product = await context.Products.SingleOrDefaultAsync(p => p.ProductID == productId);
			product.Quantity += 1;

			await context.SaveChangesAsync();
		}

		public async Task<List<Image>> GetImagesAsync(int productId)
		{
			return await context.Images.Where(i => i.ProductID == productId).ToListAsync();
		}

		public async Task<List<Product>> GetLatestAsync(int productCount)
		{
			return await context.Products.OrderByDescending(p => p.DateReleased).Take(productCount).ToListAsync();
		}

		public async Task AddImageAsync(Image image)
		{
			context.Images.Add(image);
			await context.SaveChangesAsync();
		}

		public async Task<Entities.File> GetFileByNameAsync(string fileName)
		{
			return await context.Files.FirstOrDefaultAsync(f => f.Name.Equals(fileName));
		}

		public async Task UploadFileAsync(Entities.File file)
		{
			context.Files.Add(file);
			await context.SaveChangesAsync();
		}

		public async Task<bool> CheckIfImageExistsAsync(Image image)
		{
			return await context.Images.AnyAsync(i => i.Name == image.Name);
		}

		public async Task<List<Product>> GetRatedProductsForUserAsync(string userId)
		{
			List<Product> ratedProducts = await context.Products.Where(p => context.RatingLogs.Where(r => r.ProductID == p.ProductID && r.UserID == userId).Any(c => true)).ToListAsync();
			return ratedProducts;
		}
	}
}
