using System.Threading.Tasks;
using Amethyst.Demo.Cards.Application.Commands;
using Amethyst.Demo.Cards.Application.Contracts.Repositories;
using Amethyst.Demo.Cards.Application.Contracts.Services;
using Microsoft.Extensions.Logging;

namespace Amethyst.Demo.Cards.Application.Services
{
    public sealed class CardService : ICardService
    {
        private readonly ICardRepository _repository;
        private readonly ILogger<CardService> _logger;

        public CardService(ICardRepository repository, ILogger<CardService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task RenameAsync(RenameCard command)
        {
            var card = await _repository.GetOrCreate(command.CardId);
            if (card.IsRemoved)
                return;
            
            card.Rename(command.Name);

            await _repository.SaveAsync(card);
            
            _logger.LogDebug($"Name changed ({command.CardId}).");
        }

        public async Task<long?> DepositAsync(IncreaseBalance command)
        {
            var card = await _repository.GetOrCreate(command.CardId);
            if (card.IsRemoved)
                return default;
            
            card.Deposit(command.Sum);
            
            if (!card.HasChanges)
                return default;

            await _repository.SaveAsync(card);
            
            _logger.LogDebug($"Balance increased ({command.CardId}).");

            return card.CurrentVersion;
        }

        public async Task<long?> WithdrawalAsync(DecreaseBalance command)
        {
            var card = await _repository.GetOrCreate(command.CardId);
            if (card.IsRemoved)
                return default;
            
            card.Withdrawal(command.Sum);
            
            if (!card.HasChanges)
                return default;

            await _repository.SaveAsync(card);
            
            _logger.LogDebug($"Balance decreased ({command.CardId}).");

            return card.CurrentVersion;
        }
    }
}