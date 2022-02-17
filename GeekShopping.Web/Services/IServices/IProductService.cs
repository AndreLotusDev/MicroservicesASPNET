using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> GetProductsModelsAsync();
        Task<ProductModel> FindProductModelByIdAsync(long id);
        Task<ProductModel> CreateProductAsync(ProductModel productToCreate);
        Task<bool> DeleteProductAsync(long id);
        Task<ProductModel> UpdateProductAsync(ProductModel productToUpdate);

    }
}
