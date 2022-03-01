using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> ProductIndex()
        {
            var productsToShow = await _productService.GetProductsModelsAsync();
            return View(productsToShow);
        }

        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                var productCreated = await _productService.CreateProductAsync(productModel);
                if(productCreated != null)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(productModel);
        }

        public async Task<IActionResult> ProductUpdate(long id)
        {
            var product = await _productService.FindProductModelByIdAsync(id);
            if(product != null)return View(product);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ProductUpdate(ProductModel productToUpdate)
        {
            if (ModelState.IsValid)
            {
                var productCreated = await _productService.UpdateProductAsync(productToUpdate);
                if (productCreated != null)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(productToUpdate);
        }

        public async Task<IActionResult> ProductDelete(long id)
        {
            var product = await _productService.FindProductModelByIdAsync(id);
            if (product != null) return View(product);
            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> ProductDelete(ProductModel productToDelete)
        {
            var productDeleted = await _productService.DeleteProductAsync(productToDelete.Id);
            return RedirectToAction(nameof(ProductIndex));
        }
    }
}
