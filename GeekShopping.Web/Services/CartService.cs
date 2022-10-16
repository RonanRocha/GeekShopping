using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;
using System.Net.Http.Headers;

namespace GeekShopping.Web.Services
{
    public class CartService : ICartService
    {

        private readonly HttpClient _httpClient;
        public const string BasePath = "api/v1/cart";

        public CartService(HttpClient client)
        {
            _httpClient = client;
        }


        private void SetToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }


        public async Task<CartViewModel> AddItemToCart(CartViewModel cartViewModel, string token)
        {
            SetToken(token);
            var response = await _httpClient.PostAsJson($"{BasePath}/add-cart", cartViewModel);
            if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong calling the API");
            return await response.ReadContentAs<CartViewModel>();
        }

        public async Task<bool> ApplyCoupon(CartViewModel cart, string couponCode, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<CartViewModel> Checkout(CartHeaderViewModel cartHeaderViewModel, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ClearCart(string userId, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<CartViewModel> FindCartByUserId(string userId, string token)
        {
            SetToken(token);
            var response = await _httpClient.GetAsync($"{BasePath}/find-cart/{userId}");
            if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong calling the API");
            return await response.ReadContentAs<CartViewModel>();
        }

        public async Task<bool> RemoveCoupon(string userId, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveFromCart(int cartId, string token)
        {
            SetToken(token);
            var response = await _httpClient.DeleteAsync($"{BasePath}/remove-cart/{cartId}");
            if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong calling the API");
            return await response.ReadContentAs<bool>();
        }

        public async Task<CartViewModel> UpdateCart(CartViewModel cartViewModel, string token)
        {
            SetToken(token);
            var response = await _httpClient.PutAsJsonAsync($"{BasePath}/update-cart", cartViewModel);
            if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong calling the API");
            return await response.ReadContentAs<CartViewModel>();
        }
    }
}
