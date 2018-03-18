using LOS.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LOS.WebApplication.Models
{
	public class CartViewModel
	{
		public Dictionary<int, List<Product>> CartItems { get; set; }
	}
}