using LOS.Bussiness.Entities;
using LOS.Bussiness.Entities.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LOS.Models
{
	public class CommentsModel
	{
		public int productId{ get; set; }
		public List<Comment> Comments { get; set; }
		public List<ApplicationUser> Users { get; set; }
		public string CurrentUserId { get; set; }
	}
}