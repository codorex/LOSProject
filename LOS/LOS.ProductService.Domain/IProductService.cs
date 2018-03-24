using LOS.DatabaseContext;
using LOS.FileModel.Domain;
using LOS.ImageModel.Domain;
using LOS.ProducModel.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LOS.ProductService.Domain
{
    public interface IProductService
    {
        Task<Product> GetAsync(int productID);

        Task<PagedList<Product>> GetListAsync(int pageNumber, int pageSize, string search = "", string sort = "");

        Task CreateAsync(Product product);

        Task UpdateAsync(Product product);

        Task DeleteAsync(int productID);

        Task<PagedList<Product>> GetListByCategoryIdAsync(int pageNumber, int pageSize, int categoryID, string keyword = "", string sort = "");

        Task<PagedList<Product>> GetListByParentCategoryIdAsync(int pageNumber, int pageSize, int categoryID, string keyword = "", string sort = "");

        Task<List<Product>> GetAllAsync();

        Task<List<Image>> GetImagesAsync(int productId);

        Task RateAsync(int productId, int rating, string userId);

        Task<decimal> GetRatingAsync(int productId);

        Task<int> GetVoteCountAsync(int productId);

        Task<bool> CheckIfUserHasVotedAsync(string userId, int productId);

        Task UpdateQuantityAsync(int productId);

        Task<List<Product>> GetLatestAsync(int productCount);

        Task AddImageAsync(Image image);

        Task UploadFileAsync(File file);

        Task<bool> CheckIfImageExistsAsync(Image image);

        Task<File> GetFileByNameAsync(string fileName);

        Task<List<Product>> GetRatedProductsForUserAsync(string userId);
    }
}
