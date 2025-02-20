using CSharpWebApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CSharpWebApi.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _products;

        public ProductService(IOptions<MongoDbSetting> mongoDbSetting)
        {
            var mongoClient = new MongoClient(
                mongoDbSetting.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                mongoDbSetting.Value.DatabaseName);

            _products = mongoDatabase.GetCollection<Product>(
                mongoDbSetting.Value.CollectionName);
        }

        public async Task<List<Product>> GetAllAsync() => await _products.Find(_ => true).ToListAsync();
        public async Task<Product?> GetByIdAsync(string id) => await _products.Find(p => p.Id == id).FirstOrDefaultAsync();
        public async Task CreateAsync(Product product) => await _products.InsertOneAsync(product);
        public async Task UpdateAsync(string id, Product product) => await _products.ReplaceOneAsync(p => p.Id == id, product);
        public async Task DeleteAsync(string id) => await _products.DeleteOneAsync(p => p.Id == id);
    }
}
