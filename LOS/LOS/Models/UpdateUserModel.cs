using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LOS.Models
{
	public class UpdateUserModel
	{
		public string Username { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public int ProductReviews { get; set; }
	}
}