using System;
using System.Threading.Tasks;
using Amethyst.Demo.Querying.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Amethyst.Demo.Querying.Host.Controllers
{
    [Route("api/cards/")]
    [ApiController]
    public sealed class CardsController : ControllerBase
    {
        private readonly ICardQuerying _querying;

        public CardsController(ICardQuerying querying)
        {
            _querying = querying;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cards = await _querying.GetAsync();

            return Ok(cards);
        }
        
        [HttpGet("{cardId}")]
        public async Task<IActionResult> Get([FromRoute]Guid cardId)
        {
            var card = await _querying.GetAsync(cardId);

            return Ok(card);
        }
    }
}