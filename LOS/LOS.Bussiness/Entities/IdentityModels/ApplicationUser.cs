using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LOS.Bussiness.Entities.IdentityModels;

namespace LOS.Bussiness.Entities.IdentityModels
{
	public class ApplicationUser : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Role { get; set; }
		public virtual ICollection<Product> Products { get; set; }

		public ApplicationUser()
		{
			this.Products = new HashSet<Product>();
		}
	}
}
