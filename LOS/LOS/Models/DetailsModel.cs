using LOS.Bussiness.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LOS.Models
{
	public class DetailsModel
	{
		public int ProductID { get; set; }
		public string Name { get; set; }
		public int CategoryID { get; set; }
		public Category Category { get; set; }
		public DateTime DateReleased { get; set; }
		public decimal Price { get; set; }
		public string Description { get; set; }
		public decimal Rating { get; set; }
		public List<Image> Images { get; set; }
		public int CommentsCount { get; set; }
		public int RatingVoteCount { get; set; }
		public bool UserHasVoted { get; set; }
		public int Quantity { get; set; }
		public int CartItemCount { get; set; }
	}
}