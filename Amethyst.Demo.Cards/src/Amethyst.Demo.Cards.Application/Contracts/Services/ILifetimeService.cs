using System.Threading.Tasks;
using Amethyst.Demo.Cards.Application.Commands;

namespace Amethyst.Demo.Cards.Application.Contracts.Services
{
    public interface ILifetimeService
    {
        Task CreateAsync(CreateCard command);
        
        Task RemoveAsync(RemoveCard command);
    }
}