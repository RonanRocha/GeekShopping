using AutoMapper;
using GeekShopping.CouponAPI.Data.ValueObjects;
using GeekShopping.CouponAPI.Model;
using GeekShopping.CouponAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CouponAPI.Repository.Implementations
{
    public class CouponRepository : ICouponRepository
    {

        private readonly AppDataContext _ctx;
        private readonly IMapper _mapper;

        public CouponRepository(AppDataContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public async Task<CouponVO> GetCouponByCouponCode(string couponCode)
        {
            Coupon coupon =  await _ctx.Coupons.FirstOrDefaultAsync(c => c.CouponCode == couponCode);
            return _mapper.Map<CouponVO>(coupon);
        }
    }
}
