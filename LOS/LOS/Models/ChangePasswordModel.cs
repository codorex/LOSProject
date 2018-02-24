using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LOS.Models
{
	public class ChangePasswordModel
	{
		public string OldPassword { get; set; }
		public string NewPassword { get; set; }
		public string NewPasswordConfirm { get; set; }
	}
}