using ETicaret.Users.Entities;

namespace ETicaret.Users.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user);

    }
}
