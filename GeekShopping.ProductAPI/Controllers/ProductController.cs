using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Helpers;
using GeekShopping.ProductAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GeekShopping.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(typeof(IEnumerable<ProductVO>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var products = await _repository.FindAllAsync();
            return Ok(products);
        }

        [HttpGet]
        [Route("Get/{id}")]
        [ProducesResponseType(typeof(ProductVO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorAPI), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(long id)
        {
            var productFound = await _repository.FindByIdAsync(id);
            if(productFound == null)
                return NotFound(new ErrorAPI { HttpStatusCode = HttpStatusCode.NotFound, DefaultMessage = "Not found any product with this id"});

            return Ok(productFound);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorAPI), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var operationOk = await _repository.DeleteAsync(id);
                return Ok(operationOk);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorAPI { HttpStatusCode = HttpStatusCode.NotFound, DefaultMessage = ex.Message });
            }
        }

        [HttpPatch]
        [Route("Update")]
        [ProducesResponseType(typeof(ProductVO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorAPI), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ErrorAPI), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Update([FromBody] ProductVO productToUpdate)
        {
            try
            {
                var update = await _repository.UpdateAsync(productToUpdate);
                if(update != null)
                    return Ok(update);
                else
                    return NotFound(new ErrorAPI { HttpStatusCode = HttpStatusCode.NotFound, DefaultMessage = "Not found any product with this information!" });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                            new ErrorAPI { HttpStatusCode = HttpStatusCode.InternalServerError, DefaultMessage = ex.Message });
            }
        }
    }
}
