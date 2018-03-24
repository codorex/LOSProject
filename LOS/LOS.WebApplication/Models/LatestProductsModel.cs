using LOS.ProducModel.Domain;
using System.Collections.Generic;

namespace LOS.WebApplication.Models
{
    public class LatestProductsModel
    {
        public List<Product> Products { get; set; }
        public List<LOS.ImageModel.Domain.Image> Images { get; set; }
    }
}