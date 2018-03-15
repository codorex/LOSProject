using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace LOS.Bussiness.Services
{
    /// <summary>
    /// Paged collection of items of type T with specified page size.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T>
    {
        public List<T> Collection { get; }
        public int TotalCount { get; }

        public PagedList(List<T> collection, int totalCount)
        {
            Collection = collection;
            TotalCount = totalCount;
        }

        public static async Task<PagedList<T>> PaginateAsync(int pageNumber, int pageSize, IOrderedQueryable<T> collection)
        {
            int totalCount = await collection.CountAsync();
            List<T> pagedList = await collection.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedList<T>(pagedList, totalCount);
        }
    }
}
