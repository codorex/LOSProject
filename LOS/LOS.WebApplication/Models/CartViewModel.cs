using LOS.ProducModel.Domain;
using System.Collections.Generic;

namespace LOS.WebApplication.Models
{
    public class CartViewModel
    {
        public Dictionary<int, List<Product>> CartItems { get; set; }
    }
}