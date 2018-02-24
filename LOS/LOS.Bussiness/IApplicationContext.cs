using LOS.Bussiness.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
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
