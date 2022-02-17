using GeekShopping.ProductAPI.Data.ValueObjects;

namespace GeekShopping.ProductAPI.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductVO>> FindAllAsync();
        Task<ProductVO> FindByIdAsync(long id);
        Task<ProductVO> CreateAsync(ProductVO productToCreate);
        Task<ProductVO> UpdateAsync(ProductVO productToUpdate);
        Task<bool> DeleteAsync(long id);
    }
}
