using AutoMapper;
using GeekShopping.CartAPI.Data.ViewModels;
using GeekShopping.CartAPI.Model;

namespace GeekShopping.CartAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mapppinConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductViewModel, Product>().ReverseMap();
                config.CreateMap<CartHeaderViewModel, CartHeader>().ReverseMap();
                config.CreateMap<CartDetailViewModel, CartDetail>().ReverseMap();
                config.CreateMap<CartViewModel, Cart>().ReverseMap();


            });
            return mapppinConfig;
        }
    }
}
