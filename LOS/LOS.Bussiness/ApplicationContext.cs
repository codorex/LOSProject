using LOS.Bussiness.Entities;
using LOS.Bussiness.Entities.IdentityModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOS.Bussiness
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>, IApplicationContext
    {
        public ApplicationContext() : base("DefaultConnection") { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
		public DbSet<RatingLog> RatingLogs { get; set; }
		public DbSet<CartItem> CartItems { get; set; }
		public DbSet<Image> Images { get; set; }
		public DbSet<File> Files { get; set; }
	}
}
