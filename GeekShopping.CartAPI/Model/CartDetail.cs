using GeekShopping.CartAPI.Model.Base;
using GeekShopping.CartAPI.Model.Context;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CartAPI.Model
{
    [Table("cart_detail")]
    public class CartDetail : BaseEntity
    {
        public int ProductId { get; set; } 
        public int CartHeaderId { get; set; }

        [ForeignKey("CartHeaderId")]
        public CartHeader CartHeader  { get; set; }

        [ForeignKey("ProductId")]
        public Product  Product { get; set; }

        public int Count { get; set; }
    }
}
