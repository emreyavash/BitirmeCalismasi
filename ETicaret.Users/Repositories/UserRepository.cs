using ETicaret.Users.Data.Interface;
using ETicaret.Users.Entities;
using ETicaret.Users.Entities.DTOs;
using ETicaret.Users.Repositories.Interface;
using ETicaret.Users.Security.Hashing;
using MongoDB.Driver;

namespace ETicaret.Users.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserContext _context;

        public UserRepository(IUserContext context)
        {
            _context = context;
        }

        public async Task AddUser(User user)
        {
            await _context.Users.InsertOneAsync(user);
        }

        public async Task<bool> DeleteUser(string id)
        {
            var user = Builders<User>.Filter.Eq(x => x.Id, id);
            DeleteResult deleteResult = await _context.Users.DeleteOneAsync(user);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount>0;
        }

        public async Task<User> GetUserById(string id)
        {
            var user = await _context.Users.Find(x=> x.Id == id).FirstOrDefaultAsync();
            return user;
        }

        public async Task<User> GetUserByMail(string mail)
        {
           var user = await _context.Users.Find(x=>x.Email == mail).FirstOrDefaultAsync();
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.Find(X => true).ToListAsync();
            return users;
        }

        public async Task<bool> UpdateUser(User user)
        {
            var updateUser = await _context.Users.ReplaceOneAsync(filter: x => x.Id == user.Id, replacement: user);
            return updateUser.IsAcknowledged && updateUser.ModifiedCount>0;

        }
        public async Task Register(UserForRegisterDTO user,string password)
        {
            var checkUser = GetUserByMail(user.Email).Result;
            if (checkUser != null)
            {
                throw new Exception("Kayıtlı kullanıcı");
            }
            byte[] passwordHash, passwordSalt;

            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var getuser = new User
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            await AddUser(getuser);
        }
    }
}
