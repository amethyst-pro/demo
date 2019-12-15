using Amethyst.Demo.Cards.Host.Models.FormModel;
using FluentValidation;

namespace Amethyst.Demo.Cards.Host.Validation
{
    public class CreateValidation : AbstractValidator<CreateFormModel>
    {
        public CreateValidation()
        {
            RuleFor(x => x.CardId)
                .NotEmpty();
            
            RuleFor(x => x.UserId)
                .NotEmpty();
   
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}