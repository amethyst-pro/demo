using Amethyst.Demo.Cards.Domain.Events.Primitives;

namespace Amethyst.Demo.Cards.Host.Models.FormModel
{
    public sealed class DepositFormModel
    {
        public decimal Amount { get; set; }
        
        public Currency Currency { get; set; }
    }
}