using ETicaret.Categories.Data.Interfaces;
using ETicaret.Categories.Entities;
using ETicaret.Categories.Settings;
using MongoDB.Driver;

namespace ETicaret.Categories.Data
{
    public class CategoryContext : ICategoryContext
    {
        public CategoryContext(ICategoryDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Categories = database.GetCollection<Category>(settings.CollectionName);
            CategoriesContextSeed.SeedData(Categories);
        }

        public IMongoCollection<Category> Categories { get; }
    }
}
