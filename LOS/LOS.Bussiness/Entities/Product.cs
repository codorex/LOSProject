﻿using LOS.Bussiness.Entities.IdentityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOS.Bussiness.Entities
{
    public class Product
    {
		[Key]
        public int ProductID { get; set; }
        public string Name { get; set; }
        public int CategoryID { get; set; }
        public DateTime DateReleased { get; set; }
        public decimal Price { get; set; }
		public string Description { get; set; }
		public decimal Rating { get; set; }
		public int Quantity { get; set; }
		public List<Image> Images { get; set; }
		public virtual ICollection<ApplicationUser> Users { get; set; }

		public Product()
		{
			this.Users = new HashSet<ApplicationUser>();
		}
	}
}
