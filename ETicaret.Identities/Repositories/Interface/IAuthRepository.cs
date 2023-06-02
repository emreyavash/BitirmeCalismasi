using ETicaret.Identities.DTOs;
using ETicaret.Identities.Security.JWT;
using ETicaret.Users.Entities;

namespace ETicaret.Identities.Repositories.Interface
{
    public interface IAuthRepository
    {
        Task<User> RegisterAsync(UserForRegisterDto userForRegisterDto,string password);
        Task<User> Login(UserForLoginDto userForLoginDto);
        Task<bool> UserExists(string email);
        AccessToken CreateAccessToken(User user);
    }
}
