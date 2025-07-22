using Microsoft.EntityFrameworkCore;
using RabbitMq.ProductAPI.Data;
using RabbitMq.ProductAPI.Models;

namespace RabbitMq.ProductAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly DbContextClass _dbContext;
        public ProductService(DbContextClass dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Product CreateProduct(Product product)
        {
            var result = _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
            return result.Entity;
        }

        public bool DeleteProduct(int id)
        {
            var filteredData = _dbContext.Products.Where(p => p.ProductId == id).FirstOrDefault();
            
            if (filteredData == null)
            {
                return false; // Product not found
            }
            else
            {
                _dbContext.Products.Remove(filteredData);
                _dbContext.SaveChanges();
                return true; // Product deleted successfully
            }
        }

        public Product GetProductById(int id)
        {
            return _dbContext.Products.Where(p => p.ProductId == id).FirstOrDefault();
        }

        public IEnumerable<Product> GetProductList()
        {
            return _dbContext.Products.ToList();
        }

        public Product UpdateProduct(Product product)
        {
            var filteredData = _dbContext.Products.Update(product);
            _dbContext.SaveChanges();
            return filteredData.Entity;
        }
    }
}
