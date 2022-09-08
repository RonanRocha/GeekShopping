using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> FindAllProducts();
        Task<ProductViewModel> FindProductById(int id);
        Task<ProductViewModel> CreateProduct(ProductViewModel productVm);
        Task<ProductViewModel> UpdateProduct(ProductViewModel productVm);
        Task<bool> DeleteProduct(int id);
    }
}
