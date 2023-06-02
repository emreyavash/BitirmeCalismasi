using ETicaret.Users.Entities;

namespace ETicaret.Identities.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user);
    }
}
