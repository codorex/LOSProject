using System.Collections.Generic;
using System.Threading.Tasks;

namespace LOS.CategoryService.Domain
{
    public interface ICategoryService
    {
        Task<Category.Domain.Category> GetAsync(int categoryID);

        Task<List<Category.Domain.Category>> GetListAsync(int pageNumber, int pageSize);

        Task CreateAsync(Category.Domain.Category category);

        Task UpdateAsync(Category.Domain.Category category);

        Task DeleteAsync(int categoryID);

        Task<List<Category.Domain.Category>> GetAllAsync();
    }
}
