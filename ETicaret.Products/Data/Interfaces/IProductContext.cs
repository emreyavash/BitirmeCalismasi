using ETicaret.Products.Entities;
using MongoDB.Driver;

namespace ETicaret.Products.Data.Interfaces
{
    public interface IProductContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
