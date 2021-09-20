using Catalog.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repositorys
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(string Id);
        Task<IEnumerable<Product>> GetProductByName(string Name);
        Task<IEnumerable<Product>> GetProductByCategory(string CategoryName);
        Task CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string Id);
    }
}
