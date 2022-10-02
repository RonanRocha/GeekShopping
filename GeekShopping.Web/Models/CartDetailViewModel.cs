using GeekShopping.CartAPI.Model.Base;
using GeekShopping.CartAPI.Model.Context;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.Web.Models
{
   
    public class CartDetailViewModel 
    {
        public int Id { get; set; }
        public int ProductId { get; set; } 
        public int CartHeaderId { get; set; }
        public CartHeaderViewModel CartHeader  { get; set; }
        public ProductViewModel  Product { get; set; }
        public int Count { get; set; }
    }
}
