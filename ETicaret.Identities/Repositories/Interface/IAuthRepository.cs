using ETicaret.Identities.DTOs;
using ETicaret.Identities.Entities;
using ETicaret.Identities.Security.JWT;
using EventBusRabbitMQ.Events;

namespace ETicaret.Identities.Repositories.Interface
{
    public interface IAuthRepository
    {
        Task<UserCreateEvent> RegisterAsync(UserForRegisterDto userForRegisterDto,string password);
        Task<User> Login(UserForLoginDto userForLoginDto);
        Task<bool> UserExists(string email);
        AccessToken CreateAccessToken(User user);
    }
}
