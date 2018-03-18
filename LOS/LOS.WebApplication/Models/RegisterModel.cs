using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LOS.Models
{
	public class RegisterModel
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string PasswordConfirm { get; set; }
		public string Email { get; set; }
		public string ReturnUrl { get; set; }
	}
}