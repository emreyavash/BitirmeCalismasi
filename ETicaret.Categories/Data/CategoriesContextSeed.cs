using ETicaret.Categories.Entities;
using MongoDB.Driver;

namespace ETicaret.Categories.Data
{
    public class CategoriesContextSeed
    {
        public static void SeedData(IMongoCollection<Category> categoryCollection)
        {
            bool existCategory = categoryCollection.Find(p => true).Any();
            if (!existCategory)
            {
                categoryCollection.InsertManyAsync(GetConfigureCategories());
            }
        }

        private static IEnumerable<Category> GetConfigureCategories()
        {
            return new List<Category>
            {
                new Category()
                {
                    CategoryName ="Telefon"
                },
                new Category()
                {
                    CategoryName = "Mobilya"
                },
                new Category(){
                    CategoryName = "Beyaz Eşya"
                }
            };
        }
    }
}
