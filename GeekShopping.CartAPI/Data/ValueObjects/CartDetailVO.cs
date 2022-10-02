using GeekShopping.CartAPI.Model.Base;
using GeekShopping.CartAPI.Model.Context;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CartAPI.Data.ValueObjects
{
   
    public class CartDetailVO 
    {
        public int Id { get; set; }
        public int ProductId { get; set; } 
        public int CartHeaderId { get; set; }
        public CartHeaderVO CartHeader  { get; set; }
        public ProductVO  Product { get; set; }
        public int Count { get; set; }
    }
}
