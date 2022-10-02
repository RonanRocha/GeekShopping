namespace GeekShopping.CartAPI.Data.ValueObjects
{
    public class CartVO
    {
        public CartHeaderVO CartHeaderVO { get; set; }
        public IEnumerable<CartDetailVO> CartDetailsVO { get; set; }
    }
}
