using ETicaret.Users.Consumers;
using ETicaret.Users.Entities;
using ETicaret.Users.Entities.DTOs;
using ETicaret.Users.Repositories.Interface;
using ETicaret.Users.Security.Hashing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ETicaret.Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UsersController> _logger;
        public UsersController(IUserRepository userRepository, ILogger<UsersController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }
        [HttpGet("GetUsers")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var userList = await _userRepository.GetUsers();
            if (userList == null)
            {
                return NotFound();
            }
            return Ok(userList);
        }

        [HttpGet("{id:length(24)}",Name ="GetUser")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<User>> GetUserById(string id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpDelete("Delete")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var deleteUser = await _userRepository.DeleteUser(id);
            return Ok(deleteUser);
        } 

        [HttpPut("Update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            var updateUser = await _userRepository.UpdateUser(user);
            return Ok(updateUser);
        }

    }
}
