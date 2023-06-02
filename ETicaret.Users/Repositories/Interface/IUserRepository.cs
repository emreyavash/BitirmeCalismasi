using ETicaret.Users.Entities;

namespace ETicaret.Users.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserById(string id);
        Task<bool> DeleteUser(string id);
        Task<bool> UpdateUser(User user);
        Task AddUser(User user);
        Task<User> GetUserByMail(string mail);
    }
}
