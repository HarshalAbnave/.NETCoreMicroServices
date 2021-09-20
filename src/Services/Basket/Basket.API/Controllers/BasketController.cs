using Basket.API.Entities;
using Basket.API.Repositorys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly ILogger<BasketController> _logger;

        public BasketController(IBasketRepository basketRepository, ILogger<BasketController> logger)
        {
            _basketRepository = basketRepository;
            _logger = logger;
        }

        [HttpGet]
        [Route("{userName}", Name ="GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket([FromRoute]string userName)
        {
            ShoppingCart basket = await _basketRepository.GetBasket(userName);
            return Ok(basket ?? new ShoppingCart(userName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart shoppingCart)
        {
            return Ok(await _basketRepository.UpdateBasket(shoppingCart));
        }

        [HttpDelete]
        [Route("{userName}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket([FromRoute] string userName)
        {
            await _basketRepository.DeleteBasket(userName);
            return Ok();
        }
    }
}
