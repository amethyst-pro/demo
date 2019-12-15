using System;
using System.Threading.Tasks;
using Amethyst.Demo.Cards.Application.Commands;
using Amethyst.Demo.Cards.Application.Contracts.Services;
using Amethyst.Demo.Cards.Domain.Events.ValueTypes;
using Amethyst.Demo.Cards.Host.Models.FormModel;
using Microsoft.AspNetCore.Mvc;

namespace Amethyst.Demo.Cards.Host.Controllers
{
    [Route("api/cards/")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ILifetimeService _lifetimeService;
        private readonly ICardService _cardService;
        
        public CardsController(
            ILifetimeService lifetimeService, 
            ICardService cardService)
        {
            _lifetimeService = lifetimeService;
            _cardService = cardService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFormModel model)
        {
            var cardId = new CardId(model.CardId);
            var userId = new UserId(model.UserId);
            var name = new CardName(model.Name);
  
            var command = new CreateCard(cardId, userId, name);

            await _lifetimeService.CreateAsync(command);

            return Ok();
        }
        
        [HttpPut("{cardId}/deposit")]
        public async Task<IActionResult> Deposit([FromRoute]Guid cardId, DepositFormModel model )
        {
            var id = new CardId(cardId);
            var money = new Money(model.Amount, model.Currency);

            var command = new IncreaseBalance(id, money);

            var version = await _cardService.DepositAsync(command);

            return Ok(version);
           
        }
        
        [HttpPut("{cardId}/withdrawal")]
        public async Task<IActionResult> Withdrawal([FromRoute]Guid cardId, WithdrawalFormModel model)
        {
            var id = new CardId(cardId);
            var money = new Money(model.Amount, model.Currency);

            var command = new DecreaseBalance(id, money);

            var version = await _cardService.WithdrawalAsync(command);

            return Ok(version);
        }
        
   
        [HttpPut("{cardId}/name")]
        public async Task<IActionResult> Rename([FromRoute]Guid cardId, RenameFormModel model)
        {
            var id = new CardId(cardId);
            var name = new CardName(model.Name);
            var command = new RenameCard(id, name);

            await _cardService.RenameAsync(command);

            return Ok();
        }
        
        [HttpDelete("{cardId}")]
        public async Task<IActionResult> Remove([FromRoute]Guid cardId)
        {
            var id = new CardId(cardId);
            var command = new RemoveCard(id);

            await _lifetimeService.RemoveAsync(command);

            return Ok();
        }
    }
}