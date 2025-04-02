using FluentValidation;

namespace PD_FOOD.Application.Commands
{
    public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
    {
        public CreateTransactionCommandValidator()
        {
            RuleFor(x => x.Type).IsInEnum();
            RuleFor(x => x.Description).NotEmpty().NotEmpty().Length(10, 100);
            RuleFor(x => x.Date).GreaterThanOrEqualTo(DateTime.Today);
            RuleFor(x => x.Amount).GreaterThan(0.0M);
        }
    }

}
