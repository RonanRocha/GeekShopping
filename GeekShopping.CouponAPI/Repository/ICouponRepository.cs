using GeekShopping.CouponAPI.Data.ValueObjects;
using GeekShopping.CouponAPI.Model;

namespace GeekShopping.CouponAPI.Repository
{
    public interface ICouponRepository
    {
        public Task<CouponVO> GetCouponByCouponCode(string couponCode);
    }
}
