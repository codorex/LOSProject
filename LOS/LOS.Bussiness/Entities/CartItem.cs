using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOS.Bussiness.Entities
{
    public class CartItem
    {
		[Key]
		public int CartItemID { get; set; }
		public string UserID { get; set; }
		public int ProductID { get; set; }
	}
}
