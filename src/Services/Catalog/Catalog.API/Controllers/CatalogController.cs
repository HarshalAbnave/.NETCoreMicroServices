using Catalog.API.Repositorys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using Catalog.API.Entities;
using System.Net;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository productRepository, ILogger<CatalogController> logger)
        {
            _repository = productRepository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            return Ok(await _repository.GetAllProducts());
        }

        [HttpGet]
        [Route("{Id:length(24)}", Name = "GetProductById")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Product>> GetProductById([FromRoute] string Id)
        {
            Product product = await _repository.GetProductById(Id);
            if(product is null)
            {
                _logger.LogError($"No Product with Id = {Id} Found");
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet]
        [Route("{CategoryName}", Name = "GetProductByCategory")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory([FromQuery] string CategoryName)
        {
            return Ok(await _repository.GetProductByCategory(CategoryName));
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            await _repository.CreateProduct(product);
            return CreatedAtRoute("GetProductById", new { Id = product.Id });
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            bool result = await _repository.UpdateProduct(product);
            if(!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete]
        [Route("{Id:length(24)}")]
        public async Task<IActionResult> DeleteProduct(string Id)
        {
            bool result = await _repository.DeleteProduct(Id);
            if(!result)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
