using ETicaret.Users.Entities;
using MongoDB.Driver;

namespace ETicaret.Users.Data.Interface
{
    public interface IUserContext
    {
        IMongoCollection<User> Users { get; }

    }
}
