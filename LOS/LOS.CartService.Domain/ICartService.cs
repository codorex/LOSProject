using LOS.ProducModel.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LOS.CartService.Domain
{
    public interface ICartService
    {
        Task<List<Product>> GetCartAsync(string userId);

        Task AddAsync(string userId, int productId);

        Task RemoveAsync(string userId, int productId);

        Task ClearAsync(string userId);
    }
}
