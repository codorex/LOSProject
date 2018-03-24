using LOS.CategoryModel.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LOS.CategoryService.Domain
{
    public interface ICategoryService
    {
        Task<Category> GetAsync(int categoryID);

        Task<List<Category>> GetListAsync(int pageNumber, int pageSize);

        Task CreateAsync(Category category);

        Task UpdateAsync(Category category);

        Task DeleteAsync(int categoryID);

        Task<List<Category>> GetAllAsync();
    }
}
