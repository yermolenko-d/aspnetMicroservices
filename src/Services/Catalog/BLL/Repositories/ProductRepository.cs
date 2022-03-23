using BLL.Repositories.Interfaces;
using DLL.Data.Interfaces;
using DLL.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _catalogContext;

        public ProductRepository(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext));
        }

        #region Get
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await (await _catalogContext.Products.FindAsync(p => true)).ToListAsync();
        }

        public async Task<Product> GetProductById(string id)
        {
            return await (await _catalogContext.Products.FindAsync(p => p.Id == id)).FirstOrDefaultAsync();
        }

        public async Task<Product> GetProductByName(string name)
        {
            var filter = Builders<Product>.Filter.ElemMatch(p => p.Name, name);

            return await(await _catalogContext.Products.FindAsync(filter)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
        {
            var filter = Builders<Product>.Filter.ElemMatch(p => p.Category, category);
            
            return await(await _catalogContext.Products.FindAsync(filter)).ToListAsync();
        }
        #endregion

        #region Modify
        public async Task CreateProduct(Product product)
        {
            await _catalogContext.Products.InsertOneAsync(product);
        }
        
        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _catalogContext.Products
                .ReplaceOneAsync(filter: x => x.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var filter = Builders<Product>.Filter.ElemMatch(p => p.Id, id);
            var result = await _catalogContext.Products.DeleteOneAsync(filter);

            return result.IsAcknowledged && result.DeletedCount > 0;
        }
        #endregion
    }
}
