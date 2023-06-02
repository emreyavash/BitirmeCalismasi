using ETicaret.Categories.Entities;
using MongoDB.Driver;

namespace ETicaret.Categories.Data.Interfaces
{
    public interface ICategoryContext
    {
        IMongoCollection<Category> Categories { get; }
    }
}
