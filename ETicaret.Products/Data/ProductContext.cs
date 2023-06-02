using ETicaret.Products.Data.Interfaces;
using ETicaret.Products.Entities;
using ETicaret.Products.Settings;
using MongoDB.Driver;

namespace ETicaret.Products.Data
{
    public class ProductContext : IProductContext
    {
        public ProductContext(IProductDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Products = database.GetCollection<Product>(settings.CollectionName);
            ProductContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
