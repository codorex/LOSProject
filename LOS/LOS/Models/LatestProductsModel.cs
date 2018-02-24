using LOS.Bussiness.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LOS.Models
{
	public class LatestProductsModel
	{
		public List<Product> Products { get; set; }
		public List<Image> Images { get; set; }
	}
}