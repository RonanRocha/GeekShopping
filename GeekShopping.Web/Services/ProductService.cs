using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;

namespace GeekShopping.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        public const string BasePath = "api/v1/product";
        public ProductService(HttpClient client)
        {
            _httpClient = client;
        }
        public async Task<ProductViewModel> CreateProduct(ProductViewModel productVm)
        {
            var response = await _httpClient.PostAsJson(BasePath, productVm);
            if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong calling the API");
            return await response.ReadContentAs<ProductViewModel>();
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BasePath}/{id}");
            if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong calling the API");
            return await response.ReadContentAs<bool>();
        }

        public async Task<IEnumerable<ProductViewModel>> FindAllProducts()
        {
            var response = await _httpClient.GetAsync(BasePath);
            if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong calling the API");
            return await response.ReadContentAs<List<ProductViewModel>>();
        }

        public async Task<ProductViewModel> FindProductById(int id)
        {
            var response = await _httpClient.GetAsync($"{BasePath}/{id}");
            if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong calling the API");
            return await response.ReadContentAs<ProductViewModel>();
        }

        public async Task<ProductViewModel> UpdateProduct(ProductViewModel productVm)
        {
            var response = await _httpClient.PutAsJson(BasePath, productVm);
            if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong calling the API");
            return await response.ReadContentAs<ProductViewModel>();
        }
    }
}
