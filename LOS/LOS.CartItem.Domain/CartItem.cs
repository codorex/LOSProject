using System.ComponentModel.DataAnnotations;

namespace LOS.CartItem.Domain
{
    public class CartItem
    {
        [Key]
        public int CartItemID { get; set; }
        public string UserID { get; set; }
        public int ProductID { get; set; }
    }
}
