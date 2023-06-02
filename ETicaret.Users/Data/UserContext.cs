using ETicaret.Users.Data.Interface;
using ETicaret.Users.Entities;
using ETicaret.Users.Settings;
using MongoDB.Driver;

namespace ETicaret.Users.Data
{
    public class UserContext:IUserContext
    {
        public UserContext(IUserDatabaseSettings settings) {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Users = database.GetCollection<User>(settings.CollectionName);

        }

        public IMongoCollection<User> Users { get;  }
    }
}
