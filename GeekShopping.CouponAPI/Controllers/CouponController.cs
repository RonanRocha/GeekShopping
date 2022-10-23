using GeekShopping.CouponAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CouponAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICouponRepository _couponRepository;

        public CouponController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;   
        }


        [HttpGet("{couponCode}")]
        [Authorize]
        public async Task<IActionResult> GetCouponByCouponCode(string couponCode)
        {
            var coupon = await _couponRepository.GetCouponByCouponCode(couponCode);
            if (coupon == null) return NotFound();
            return Ok(coupon);
        }
    }
}
