using LOS.Domain.Models.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace LOS.Bussiness.Services
{
    public static class ProductPagedListExtensions
    {
        /// <summary>
        /// Returns a paged list of Products, sorted by the passed parameter.
        /// </summary>
        /// <param name="collection">Filtered collection that is ready to be sorted.</param>
        /// <param name="sortOrder">Determines the order in which the collection will be sorted.</param>
        /// <param name="pageNumber">Number of the current page.</param>
        /// <param name="pageSize">The amount of items displayed on the page.</param>
        public static async Task<PagedList<Product>> SortAsync(this PagedList<Product> pagedList, IQueryable<Product> collection, string sortOrder, int pageNumber, int pageSize)
        {
            switch (sortOrder)
            {
                case "name_asc":
                    return await PagedList<Product>.PaginateAsync(pageNumber, pageSize, collection.OrderBy(p => p.Name));
                case "name_desc":
                    return await PagedList<Product>.PaginateAsync(pageNumber, pageSize, collection.OrderByDescending(p => p.Name));
                case "price_asc":
                    return await PagedList<Product>.PaginateAsync(pageNumber, pageSize, collection.OrderBy(p => p.Price));
                case "price_desc":
                    return await PagedList<Product>.PaginateAsync(pageNumber, pageSize, collection.OrderByDescending(p => p.Price));
                case "topRated":
                    return await PagedList<Product>.PaginateAsync(pageNumber, pageSize, collection.OrderByDescending(p => p.Rating));
                default:
                    return await PagedList<Product>.PaginateAsync(pageNumber, pageSize, collection.OrderBy(p => p.DateReleased));
            }
        }
    }
}