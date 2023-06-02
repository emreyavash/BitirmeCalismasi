using ETicaret.Categories.Data.Interfaces;
using ETicaret.Categories.Entities;
using ETicaret.Categories.Repositories.Interface;
using MongoDB.Driver;

namespace ETicaret.Categories.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ICategoryContext _context;

        public CategoryRepository(ICategoryContext context)
        {
            _context = context;
        }

        public async Task Add(Category category)
        {
            await _context.Categories.InsertOneAsync(category);
        }

        public async Task<bool> Delete(string id)
        {
            var filter = Builders<Category>.Filter.Eq(c => c.Id, id);
            DeleteResult deleteResult= await _context.Categories.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.Find(c => true).ToListAsync();
        }

        public async Task<Category> GetCategoryById(string id)
        {
            return await _context.Categories.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> Update(Category category)
        {
            var updateResult = await _context.Categories.ReplaceOneAsync(filter: c => c.Id == category.Id, replacement: category);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
