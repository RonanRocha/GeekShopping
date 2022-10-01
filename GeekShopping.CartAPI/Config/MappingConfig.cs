using AutoMapper;
//using GeekShopping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Model;

namespace GeekShopping.CartAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mapppinConfig = new MapperConfiguration(config =>
            {
                //config.CreateMap<ProductVO, Product>();
                //config.CreateMap<Product, ProductVO>();

            });
            return mapppinConfig;
        }
    }
}
