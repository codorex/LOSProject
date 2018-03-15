using LOS.Domain.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LOS.Bussiness.Services.CartServices
{
    public interface ICartService
    {
        Task<List<Product>> GetCartAsync(string userId);

        Task AddAsync(string userId, int productId);

        Task RemoveAsync(string userId, int productId);

        Task ClearAsync(string userId);
    }
}
