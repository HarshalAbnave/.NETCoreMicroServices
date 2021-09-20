using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositorys
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _catalogContext;

        public ProductRepository(ICatalogContext context)
        {
            _catalogContext = context;
        }

        public async Task CreateProduct(Product product)
        {
            await _catalogContext.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string Id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, Id);
            DeleteResult deleteResult = await _catalogContext.Products.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _catalogContext.Products.Find(p => true).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string CategoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, CategoryName);
            return await _catalogContext.Products.Find(filter).ToListAsync();
        }

        public async Task<Product> GetProductById(string Id)
        {
            return await _catalogContext.Products.Find(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string Name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name , Name);
            return await _catalogContext.Products.Find(filter).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _catalogContext.Products.ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
