using AutoMapper;
using GeekShopping.CouponAPI.Data.ValueObjects;
using GeekShopping.CouponAPI.Model;

namespace GeekShopping.CouponAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mapppinConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponVO, Coupon>().ReverseMap();
                //config.CreateMap<CartHeaderViewModel, CartHeader>().ReverseMap();
                //config.CreateMap<CartDetailViewModel, CartDetail>().ReverseMap();
                //config.CreateMap<CartViewModel, Cart>().ReverseMap();


            });
            return mapppinConfig;
        }
    }
}
