using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;

namespace GeekShopping.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _client;
        private readonly ILogger<ProductService> _logger;
        public const string BasePath = "api/v1/Product";

        public ProductService(HttpClient client, ILogger<ProductService> logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ProductModel> CreateProductAsync(ProductModel productToCreate)
        {
            var response = await _client.PostAsJsonAsync($"{BasePath}/Create", productToCreate);
            return await response.ReadContentAs<ProductModel>();

        }

        public async Task<bool> DeleteProductAsync(long id)
        {
            var response = await _client.DeleteAsync($"{BasePath}/Delete/{id}");
            return await response.ReadContentAs<bool>();
        }

        public async Task<ProductModel> UpdateProductAsync(ProductModel productToUpdate)
        {
            var response = await _client.PutAsJsonAsync($"{BasePath}/Update", productToUpdate);
            return await response.ReadContentAs<ProductModel>();
        }

        public async Task<ProductModel> FindProductModelByIdAsync(long id)
        {
            var response = await _client.GetAsync($"{BasePath}/Get/{id}");
            return await response.ReadContentAs<ProductModel>();
        }

        public async Task<IEnumerable<ProductModel>> GetProductsModelsAsync()
        {
            var response = await _client.GetAsync($"{BasePath}/GetAll");
            return await response.ReadContentAs<List<ProductModel>>();
        }
    }
}
