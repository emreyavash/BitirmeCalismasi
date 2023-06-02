using ETicaret.Identities.DTOs;
using ETicaret.Identities.Repositories.Interface;
using ETicaret.Users.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ETicaret.Identities.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthRepository authRepository, ILogger<AuthController> logger)
        {
            _authRepository = authRepository;
            _logger = logger;
        }

        [HttpPost("Register")]
        [ProducesResponseType(typeof(User),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<User>> Register(UserForRegisterDto userForRegisterDto)
        {
            var userExist = await _authRepository.UserExists(userForRegisterDto.Email);
            if (!userExist)
            {
                _logger.LogError("Kayıtlı Kullanıcı");
                return BadRequest();
            }
            var registerResult = _authRepository.RegisterAsync(userForRegisterDto, userForRegisterDto.Password);
            var result = _authRepository.CreateAccessToken(registerResult.Result);

            if(result !=null)
            {
                return Ok(result);
            }
            return BadRequest("Kayıt olunamadı");
        }
    }
}
