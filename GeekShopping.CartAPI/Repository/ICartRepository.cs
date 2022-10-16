using GeekShopping.CartAPI.Data.ViewModels;

namespace GeekShopping.CartAPI.Repository
{
    public interface ICartRepository
    {
        Task<CartViewModel> FindCartByUserId(string userId);
        Task<CartViewModel> SaveOrUpdateCart(CartViewModel cart);
        Task<bool> RemoveFromCart(int cartDetailsId);
        Task<bool> ApplyCoupon(string userId, string couponCode);
        Task<bool> RemoveCoupon(string userId);
        Task<bool> ClearCart(string userId);
    }
}
