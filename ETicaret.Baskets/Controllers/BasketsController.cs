using ETicaret.Baskets.Entities;
using ETicaret.Baskets.Repositories.Interface;
using EventBusRabbitMQ.Core;
using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Producer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace ETicaret.Baskets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly EventBusRabbitMQProducer _eventBus;
        private readonly ILogger<BasketsController> _logger;

        public BasketsController(IBasketRepository basketRepository, EventBusRabbitMQProducer eventBus, ILogger<BasketsController> logger)
        {
            _basketRepository = basketRepository;
            _eventBus = eventBus;
            _logger = logger;
        }

        //[HttpGet("BasketsByUserId")]
        //[ProducesResponseType(typeof(IEnumerable<Basket>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //public async Task<ActionResult<IEnumerable<Basket>>> GetBasketsByUserId(string UserId)
        //{
        //    var userBasket = await _basketRepository.GetBasketsByUserId(UserId);
        //    if (userBasket == null)
        //    {
        //        return BadRequest();
        //    }
        //    return Ok(userBasket);
        //}  
        [HttpGet("BasketByUserId")]
        [ProducesResponseType(typeof(UserBasket), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<UserBasket>> GetBasketByUserId(string userId)
        {
            var basket = await _basketRepository.GetBasketByUserId(userId);
            if (basket == null)
            {
                return BadRequest();
            }
            return Ok(basket);
        }
        [HttpPost("CompleteBasket")]
        [ProducesResponseType(typeof(UserBasket), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<UserBasket>> CompleteBasket(string userId)
        {
            var basket = await _basketRepository.GetBasketByUserId(userId);
            OrderCreateEvent eventMessage = new OrderCreateEvent();
            eventMessage.UserId = basket.UserId;
            decimal totalPrice = 0;
            for (int i = 0; i < basket.Baskets.Count; i++)
            {
                totalPrice += basket.Baskets[i].TotalPrice;
            }
            eventMessage.TotalPrice = totalPrice;
            eventMessage.OrderComplete = false;
            eventMessage.CreatedAt = DateTime.UtcNow;
            try
            {
                _eventBus.Publish(EventBusConstants.OrderCreateQueue, eventMessage);
                await _basketRepository.DeleteBasket(userId);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "ERROR Publishing integration event: {EventId} from {AppName}", eventMessage.Id, "Ticaret");
                throw;
            }
            if (basket == null)
            {
                return BadRequest();
            }
            return Ok("Ödeme sayfasına gidiliyor.");
        }
        [HttpDelete("DeleteBasket")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteBasket(string id)
        {
            var basket = await _basketRepository.DeleteBasket(id);
           
            return Ok(basket);
        }
        [HttpPost("AddItem")]
        [ProducesResponseType(typeof(Basket), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Basket>> AddItem([FromBody] Basket basket,string userId)
        {
            var item = await _basketRepository.GetBasketByUserId(userId);
            if (item == null)
            {
                item = new UserBasket(userId);
            }
            item.Baskets.Add(basket);
            await _basketRepository.SetBasket(item);
            return Ok();
        }
        [HttpPut("Update")]
        [ProducesResponseType(typeof(UserBasket), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<UserBasket>> SetBasket([FromBody] UserBasket basket)
        {
            var item =await _basketRepository.SetBasket(basket);
            if (!item)
            {
                return BadRequest();
            }
            return Ok(item);
        }
      
    }
}
