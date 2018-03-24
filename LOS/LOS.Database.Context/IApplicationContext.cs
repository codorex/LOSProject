using LOS.CartItemModel.Domain;
using LOS.CategoryModel.Domain;
using LOS.CommentModel.Domain;
using LOS.FileModel.Domain;
using LOS.ImageModel.Domain;
using LOS.ProducModel.Domain;
using LOS.RatingLogModel.Domain;
using System.Data.Entity;
using System.Threading.Tasks;

namespace LOS.DatabaseContext
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
