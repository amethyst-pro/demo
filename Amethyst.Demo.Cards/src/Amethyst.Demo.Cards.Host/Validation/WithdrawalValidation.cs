using Amethyst.Demo.Cards.Host.Models.FormModel;
using FluentValidation;

namespace Amethyst.Demo.Cards.Host.Validation
{
    public sealed class WithdrawalValidation : AbstractValidator<WithdrawalFormModel>
    {
        public WithdrawalValidation()
        {
            RuleFor(x => x.Amount)
                .NotEmpty();

            RuleFor(x => x.Currency)
                .IsInEnum();
        }
    }
}