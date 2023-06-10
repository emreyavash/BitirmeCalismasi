
using ETicaret.Identities.Entities;

namespace ETicaret.Identities.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user);
    }
}
