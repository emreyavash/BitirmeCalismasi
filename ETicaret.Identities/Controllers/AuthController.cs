using AutoMapper;
using ETicaret.Identities.DTOs;
using ETicaret.Identities.Repositories.Interface;
using EventBusRabbitMQ.Core;
using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Producer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;

namespace ETicaret.Identities.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly EventBusRabbitMQProducer _eventBus;
        public AuthController( ILogger<AuthController> logger, EventBusRabbitMQProducer eventBus)
        {
            _logger = logger;
            _eventBus = eventBus;
        }

        [HttpPost("Register")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public  ActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            UserCreateEvent eventMessage = new UserCreateEvent();
            eventMessage.Email = userForRegisterDto.Email;
            eventMessage.FirstName = userForRegisterDto.FirstName;
            eventMessage.LastName = userForRegisterDto.LastName;
            eventMessage.Password = userForRegisterDto.Password;        
            try
            {
                _eventBus.Publish(EventBusConstants.UserCreateQueue,eventMessage);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "ERROR Publishing integration event: {EventFirstname} from {AppName}", eventMessage.FirstName, "Ticaret");
                throw;
            }
            return Ok("Başarılı bir şekilde kayıt oluşturuldu.");
        }
    }
}
