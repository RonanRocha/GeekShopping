using GeekShopping.ProductAPI.Data.ValueObjects;

namespace GeekShopping.ProductAPI.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductVO>> FindAll();
        Task<ProductVO> FindById(int id);
        Task<ProductVO> Create(ProductVO productVo);
        Task<ProductVO> Update(ProductVO productVo);
        Task<bool> Delete(int id);
    }
}
