using LOS.Domain.Models.Entities;
using System.Collections.Generic;

namespace LOS.Models
{
    public class UpdateUserModel
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int ProductReviews { get; set; }
        public List<Product> RatedProducts { get; set; }
        public List<Product> ReviewedProducts { get; set; }

        public UpdateUserModel()
        {
            RatedProducts = new List<Product>();
            ReviewedProducts = new List<Product>();
        }
    }
}