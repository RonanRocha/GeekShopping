namespace GeekShopping.Web.Models;
    public class CartViewModel
    {
        public CartHeaderViewModel CartHeaderVO { get; set; }
        public IEnumerable<CartDetailViewModel> CartDetailsVO { get; set; }
    }
}
