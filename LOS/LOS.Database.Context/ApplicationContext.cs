﻿using LOS.CartItemModel.Domain;
using LOS.CategoryModel.Domain;
using LOS.CommentModel.Domain;
using LOS.FileModel.Domain;
using LOS.ImageModel.Domain;
using LOS.ProducModel.Domain;
using LOS.RatingLogModel.Domain;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace LOS.DatabaseContext
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
