using ETicaret.Products.Entities;
using ETicaret.Products.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ETicaret.Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<Product> _logger;

        public ProductController(IProductRepository productRepository, ILogger<Product> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        [HttpGet("getProducts")]
        [ProducesResponseType(typeof(Product),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepository.GetProducts();
            return Ok(products);
        }

        [HttpGet("getProductByName")]
        [ProducesResponseType(typeof(Product),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByName(string name)
        {
            var products = await _productRepository.GetProductByName(name);
            if (!products.Any())
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpGet("{id:length(24)}", Name = "GetProductById")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost("add")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> AddProducts([FromBody] Product product)
        {
            await _productRepository.AddProduct(product);
            return Ok("Başarılı bir şekilde eklenmiştir.");
        }

        [HttpDelete("delete")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeletedProduct(string id)
        {
            var product = await _productRepository.DeleteProduct(id);
            return Ok(product);
        }

        [HttpPut("update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            var updateProduct = await _productRepository.UpdateProduct(product);
            return Ok(updateProduct);
        }
    }
}
