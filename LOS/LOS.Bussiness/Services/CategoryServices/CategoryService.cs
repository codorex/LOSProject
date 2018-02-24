using LOS.Bussiness.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace LOS.Bussiness.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationContext context;

        public CategoryService(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task CreateAsync(Category category)
        {
            context.Categories.Add(category);

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int categoryID)
        {
            Category category = await context.Categories.SingleOrDefaultAsync(c => c.CategoryID == categoryID);
            context.Categories.Remove(category);

            await context.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAllAsync()
        {
            List<Category> categories = await context.Categories.ToListAsync();

            return categories;
        }

        public async Task<Category> GetAsync(int categoryID)
        {
            Category category = await context.Categories.SingleOrDefaultAsync(c => c.CategoryID == categoryID);

            return category;
        }

        public async Task<List<Category>> GetListAsync(int pageNumber, int pageSize)
        {
            List<Category> categories = await context.Categories.OrderBy(c => c.CategoryID).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return categories;
        }

        public async Task UpdateAsync(Category category)
        {
            Category queryCategory = await context.Categories.SingleOrDefaultAsync(c => c.CategoryID == category.CategoryID);
            queryCategory.Name = category.Name;
            queryCategory.ParentCategoryID = category.ParentCategoryID;

            await context.SaveChangesAsync();
        }
    }
}
