using System.Threading.Tasks;
using Amethyst.Demo.Cards.Application.Commands;

namespace Amethyst.Demo.Cards.Application.Contracts.Services
{
    public interface ICardService
    {
        Task RenameAsync(RenameCard command);

        Task<long?> DepositAsync(IncreaseBalance command);

        Task<long?> WithdrawalAsync(DecreaseBalance command);
    }
}