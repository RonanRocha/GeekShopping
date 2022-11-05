using GeekShopping.Web.Models;
using GeekShopping.Web.Utils;
using System.Net.Http.Headers;

namespace GeekShopping.Web.Services.IServices
{
    public class CouponService : ICouponService
    {

        private readonly HttpClient _httpClient;
        public const string BasePath = "api/v1/coupon";
       
        public CouponService(HttpClient client)
        {
            _httpClient = client;
        }

        private void SetToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }


        public async Task<CouponViewModel> GetCoupon(string couponCode, string token)
        {

            SetToken(token);
            var response = await _httpClient.GetAsync($"{BasePath}/{couponCode}");
            if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong calling the API");
            return await response.ReadContentAs<CouponViewModel>();

        }
    }
}
