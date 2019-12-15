using Amethyst.Demo.Cards.Host.Models.FormModel;
using FluentValidation;

namespace Amethyst.Demo.Cards.Host.Validation
{
    public class RenameValidation : AbstractValidator<RenameFormModel>
    {
        public RenameValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}