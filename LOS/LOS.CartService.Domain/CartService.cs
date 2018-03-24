using LOS.CartItemModel.Domain;
using LOS.DatabaseContext;
using LOS.ProducModel.Domain;
using LOS.ProductService.Domain;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace LOS.CartService.Domain
{
    public class CartService : ICartService
    {
        private readonly ApplicationContext context;
        private readonly IProductService productService;

        public CartService(ApplicationContext context, IProductService productService)
        {
            this.context = context;
            this.productService = productService;
        }

        public async Task<List<Product>> GetCartAsync(string userId)
        {
            List<CartItem> cartItems = await context.CartItems.Where(c => c.UserID == userId).ToListAsync();
            List<Product> products = new List<Product>();

            foreach (var cartItem in cartItems)
            {
                if (cartItem.UserID == userId)
                {
                    products.Add(await productService.GetAsync(cartItem.ProductID));
                }
            }

            return products;
        }

        public async Task AddAsync(string userId, int productId)
        {
            Product product = await productService.GetAsync(productId);

            if (product.Quantity > 0)
            {
                product.Quantity -= 1;

                await productService.UpdateAsync(product);
                context.CartItems.Add(new CartItem { UserID = userId, ProductID = productId });
                await context.SaveChangesAsync();
            }
        }

        public async Task RemoveAsync(string userId, int productId)
        {
            Product product = await productService.GetAsync(productId);
            await productService.UpdateQuantityAsync(productId);

            context.CartItems.Remove(await context.CartItems.FirstOrDefaultAsync(c => c.UserID == userId && c.ProductID == productId));
            await context.SaveChangesAsync();
        }

        public async Task ClearAsync(string userId)
        {
            ICollection<CartItem> items = await context.CartItems.Where(c => c.UserID == userId).ToListAsync();

            foreach (var item in items)
            {
                await productService.UpdateQuantityAsync(item.ProductID);
                context.CartItems.Remove(item);
            }

            await context.SaveChangesAsync();
        }
    }
}
