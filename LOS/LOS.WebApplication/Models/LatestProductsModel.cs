using LOS.Domain.Models.Entities;
using System.Collections.Generic;

namespace LOS.Models
{
    public class LatestProductsModel
    {
        public List<Product> Products { get; set; }
        public List<Image> Images { get; set; }
    }
}