using LOS.Domain.Models.Entities;
using System.Data.Entity;
using System.Threading.Tasks;

namespace LOS.Bussiness
{
    public interface IApplicationContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<RatingLog> RatingLogs { get; set; }
        DbSet<CartItem> CartItems { get; set; }
        DbSet<Image> Images { get; set; }
        DbSet<File> Files { get; set; }

        Task<int> SaveChangesAsync();
    }
}
