using Amethyst.Demo.Cards.Host.Models.FormModel;
using FluentValidation;

namespace Amethyst.Demo.Cards.Host.Validation
{
    public class DepositValidation : AbstractValidator<DepositFormModel>
    {
        public DepositValidation()
        {
            RuleFor(x => x.Amount)
                .NotEmpty();

            RuleFor(x => x.Currency)
                .IsInEnum();
        }
    }
}