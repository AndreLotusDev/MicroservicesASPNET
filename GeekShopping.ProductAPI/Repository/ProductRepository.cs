using AutoMapper;
using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Model.Base;
using GeekShopping.ProductAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper _mapper;
        private readonly MySQLContext _context;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(IMapper mapper, MySQLContext context, ILogger<ProductRepository> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ProductVO> Create(ProductVO productToCreate)
        {
            try
            {
                var product = _mapper.Map<Product>(productToCreate);
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();

                return productToCreate;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating a new product: {ex.Message}");
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var productToDelete = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (productToDelete == null)
            {
                _logger.LogError($"Id without correlated information in the DB :{id}");
                return false;
            }

            try
            {
                await Task.Run(() => _context.Products.Remove(productToDelete));
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something goes wrong while deleting the product entity");
                throw new Exception(ex.Message) ;
            }
        }

        public async Task<IEnumerable<ProductVO>> FindAllAsync()
        {
            var productsToReturn = await _context.Products.ToListAsync();
            return _mapper.Map<IEnumerable<ProductVO>>(productsToReturn);
        }

        public async Task<ProductVO> FindByIdAsync(long id)
        {
            var product = await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
            if(product == null)
            {
                _logger.LogWarning("The id was not found any correlation with the products in the database");
                return null;
            }

            return _mapper.Map<ProductVO>(product);
        }

        public async Task<ProductVO> UpdateAsync(ProductVO productToUpdate)
        {
            try
            {
                var product = _mapper.Map<Product>(productToUpdate);
                await Task.Run(() => _context.Products.Update(product));
                await _context.SaveChangesAsync();

                return productToUpdate;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error with the update of the product: {ex.Message}");
                throw new Exception(ex.Message);
            }
            
        }
    }
}
