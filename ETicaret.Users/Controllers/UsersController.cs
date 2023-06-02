using ETicaret.Users.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly Logger<UsersController> _logger;

        public UsersController(IUserRepository userRepository, Logger<UsersController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }
    }
}
