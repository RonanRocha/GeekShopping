namespace GeekShopping.CartAPI.Data.ViewModels
{
    public class CartViewModel
    {
        public CartHeaderViewModel CartHeader { get; set; }
        public IEnumerable<CartDetailViewModel> CartDetails { get; set; }
    }
}
