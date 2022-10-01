namespace GeekShopping.CartAPI.Model
{
    public class Cart
    {
        public CartHeader Cartheader { get; set; }
        public IEnumerable<CartDetail> CartDetails { get; set; }
    }
}
