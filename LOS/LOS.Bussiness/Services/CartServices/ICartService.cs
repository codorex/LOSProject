using LOS.Bussiness.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
