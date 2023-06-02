using ETicaret.Categories.Entities;

namespace ETicaret.Categories.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategoryById(string id);
        
        Task Add(Category category);
        Task<bool> Update(Category category);
        Task<bool> Delete(string id);

    }
}
