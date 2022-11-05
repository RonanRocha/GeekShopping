using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{
    public class CartController : Controller
    {

        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly ICouponService _couponService;

        public CartController(IProductService productService, ICartService cartService, ICouponService couponService)
        {
            _productService = productService;
            _cartService = cartService;
            _couponService = couponService;
        }


        private async Task<string> GetToken()
        {

            var token = await HttpContext.GetTokenAsync("access_token");
            return token;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var cart = await FindUserCart();
            if (cart == null) return NotFound();
            return View(cart);
        }


        [HttpPost]
        public async Task<IActionResult> ApplyCoupon(CartViewModel cartViewModel)
        {
            var token = await GetToken();
            var userId = User.Claims.Where(x => x.Type == "sub")?.FirstOrDefault()?.Value;

            var response = await _cartService.ApplyCoupon(cartViewModel, token);
           
            if(response)
            {
                return RedirectToAction("Index");
            }

            return View();
         
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCoupon()
        {
            var token = await GetToken();
            var userId = User.Claims.Where(x => x.Type == "sub")?.FirstOrDefault()?.Value;

            var response = await _cartService.RemoveCoupon(userId, token);

            if (response)
            {
                return RedirectToAction("Index");
            }

            return View();
        }



        [Authorize]
        public async Task<IActionResult> Remove(int id)
        {
            var token = await GetToken();
            var response = await _cartService.RemoveFromCart(id, token);
            if(response)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }


        private async  Task<CartViewModel?> FindUserCart()
        {
            var token = await GetToken();
            var userId = User.Claims.Where(x => x.Type == "sub")?.FirstOrDefault()?.Value;

            var response = await _cartService.FindCartByUserId(userId, token);

            if (response?.CartHeader != null)
            {

                if(!String.IsNullOrEmpty(response?.CartHeader.CouponCode))
                {
                    var coupon = await _couponService.GetCoupon(response.CartHeader.CouponCode, token);

                    if(coupon?.CouponCode != null)
                    {
                        response.CartHeader.DiscountTotal = coupon.DiscountAmount;
                    }
                }

                foreach (var detail in response.CartDetails)
                {
                    response.CartHeader.PurchaseAmount += (detail.Product.Price * detail.Count);
                }

                response.CartHeader.PurchaseAmount -= response.CartHeader.DiscountTotal;
            }

            return response;
        }
    }
}
