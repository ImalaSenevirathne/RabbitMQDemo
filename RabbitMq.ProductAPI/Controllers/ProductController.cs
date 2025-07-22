using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMq.ProductAPI.Models;
using RabbitMq.ProductAPI.RabbitMQ;
using RabbitMq.ProductAPI.Services;

namespace RabbitMq.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IRabbitMQProducer _rabbitMQProducer;
        public ProductController(IProductService productService, IRabbitMQProducer rabbitMQProducer)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _rabbitMQProducer = rabbitMQProducer ?? throw new ArgumentNullException(nameof(rabbitMQProducer));
        }
        [HttpGet("productlist")]
        public IEnumerable<Product> GetProducts()
        {
            var products = _productService.GetProductList();
            return products;
        }
        [HttpGet("getproductbyid/{id}")]
        public Product GetProductById(int id)
        {
            return _productService.GetProductById(id);
        }

        [HttpPost("addproduct")]
        public Product CreateProduct(Product product)
        {
            var createdProduct = _productService.CreateProduct(product);

            // Send the inserted product data to the queue and consumer will listening this data from queue
            _rabbitMQProducer.SendProductMessage(createdProduct);

            return createdProduct;
        }

        [HttpPut("updateproduct")]
        public ActionResult<Product> UpdateProduct(Product product)
        {            
            return _productService.UpdateProduct(product);
        }

        [HttpDelete("deleteproduct/{id}")]
        public bool DeleteProduct(int id)
        {
            return _productService.DeleteProduct(id);
        }
    }
}
