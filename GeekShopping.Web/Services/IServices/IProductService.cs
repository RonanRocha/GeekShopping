using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> FindAllProducts();
        Task<ProductViewModel> FindProductById(int id, string token);
        Task<ProductViewModel> CreateProduct(ProductViewModel productVm, string token);
        Task<ProductViewModel> UpdateProduct(ProductViewModel productVm, string token);
        Task<bool> DeleteProduct(int id, string token);
    }
}
