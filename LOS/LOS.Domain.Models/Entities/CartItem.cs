using System.ComponentModel.DataAnnotations;

namespace LOS.Domain.Models.Entities
{
    public class CartItem
    {
        [Key]
        public int CartItemID { get; set; }
        public string UserID { get; set; }
        public int ProductID { get; set; }
    }
}
